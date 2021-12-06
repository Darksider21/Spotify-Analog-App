using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SpotifyAnalogApp.Business.DTO.RequestDto;
using SpotifyAnalogApp.Business.DTO.ResponceDTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SpotifyAnalogApp.Web.Configurations;
using SpotifyAnalogApp.Data.Data;
using Microsoft.EntityFrameworkCore;
using SpotifyAnalogApp.Data.Models;

namespace SpotifyAnalogApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly JwtConfig jwtConfig;
        private readonly TokenValidationParameters tokenValidationParams;
        private readonly SpotifyAnalogAppContext DbContext;


        public AuthManagementController(UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor, TokenValidationParameters tokenValidationParams,
            SpotifyAnalogAppContext DbContext)
        {
            this.userManager = userManager;
            this.jwtConfig = optionsMonitor.CurrentValue;
            this.tokenValidationParams = tokenValidationParams;
            this.DbContext = DbContext;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto userRequest)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await userManager.FindByEmailAsync(userRequest.Email);
                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Success = false,
                        Errors = new List<string>(){
                                            "Email already exist"
                                        }
                    });
                }

                var newUser = new IdentityUser() { Email = userRequest.Email, UserName = userRequest.Email };
                var isCreated = await userManager.CreateAsync(newUser, userRequest.Password);
                if (isCreated.Succeeded)
                {

                    return Ok(await GenerateJwtToken(newUser));

                }
                return new JsonResult(new RegistrationResponse()
                {
                    Success = false,
                    Errors = isCreated.Errors.Select(x => x.Description).ToList()
                }
                    )
                { StatusCode = 500 };
            }

            return BadRequest(new RegistrationResponse()
            {
                Success = false,
                Errors = new List<string>()
                {
                "Invalid payload"
                }
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userRequest)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await userManager.FindByEmailAsync(userRequest.Email);

                if (existingUser == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Success = false,
                        Errors = new List<string>(){
                                        "Invalid authentication request"
                    } });
                }

                var passwordIsCorrect = await userManager.CheckPasswordAsync(existingUser, userRequest.Password);

                if (passwordIsCorrect)
                {
                    return Ok(await GenerateJwtToken(existingUser));

                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Success = false,
                        Errors = new List<string>(){
                                         "Invalid authentication request"
                    } });
                }
            }

            return BadRequest(new RegistrationResponse()
            {
                Success = false,
                Errors = new List<string>(){
                                        "Invalid payload"
                                    }
            });
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest token)
        {
            if (ModelState.IsValid)
            {
                var res = await VerifyAndGenerateToken(token);
                if (res == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "invalid tokens"
                        },
                        Success= false
                    });
                }

                return Ok(res);
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>()
                        {
                            "invalid payload"
                        },
                Success = false
            });
        }


        private async Task<AuthResult> GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10), // 5-10 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = user.Id,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                Token = RandomString(35) + Guid.NewGuid()
            };

            await DbContext.RefreshTokens.AddAsync(refreshToken);
            await DbContext.SaveChangesAsync();

            return new AuthResult()
            {
                Token = jwtToken,
                Success = true,
                RefreshToken = refreshToken.Token
            };
        }

        private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Validation 1 - Validation JWT token format
                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, tokenValidationParams, out var validatedToken);

                // Validation 2 - Validate encryption alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        return null;
                    }
                }

                // Validation 3 - validate expiry date
                var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.UtcNow)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Token has not yet expired"
                        }
                    };
                }

                // validation 4 - validate existence of the token
                var storedToken = await DbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

                if (storedToken == null)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Token does not exist"
                        }
                    };
                }

                // Validation 5 - validate if used
                if (storedToken.IsUsed)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Token has been used"
                        }
                    };
                }

                // Validation 6 - validate if revoked
                if (storedToken.IsRevoked)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Token has been revoked"
                        }
                    };
                }

                // Validation 7 - validate the id
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if (storedToken.JwtId != jti)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Token doesn't match"
                        }
                    };
                }

                // update current token 

                storedToken.IsUsed = true;
                DbContext.RefreshTokens.Update(storedToken);
                await DbContext.SaveChangesAsync();

                // Generate a new token
                var dbUser = await userManager.FindByIdAsync(storedToken.UserId);
                return await GenerateJwtToken(dbUser);
            }
            catch (Exception ex)
            {
                
                
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Something went wrong.",
                            ex.Message
                        }
                    };
                
            }


            

        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(x => x[random.Next(x.Length)]).ToArray());
        }

    } 
}

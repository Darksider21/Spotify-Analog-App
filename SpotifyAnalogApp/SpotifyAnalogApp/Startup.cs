using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SpotifyAnalogApp.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SpotifyAnalogApp.Data.Repositiry;
using SpotifyAnalogApp.Data.Repositiry.Base;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using SpotifyAnalogApp.Business.Services;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.Text.Json.Serialization;

namespace SpotifyAnalogApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //data
            ConfigureDatabases(services);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ISongRepository, SongRepository>();

            //Buisenes
            services.AddScoped<ISongService, SongService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddControllers();
            //services.AddControllers().AddNewtonsoftJson(options =>
            //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "deleteme", Version = "v1" });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "deleteme v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            

           
        }

        public void ConfigureDatabases(IServiceCollection services)
        {
            // use in-memory database
            //services.AddDbContext<SpotifyAnalogAppContext>(c =>
            //    c.UseInMemoryDatabase("AspnetRunConnection"));

            //// use real database
            services.AddDbContext<SpotifyAnalogAppContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("AspnetRunConnection")));
        }
    }
}

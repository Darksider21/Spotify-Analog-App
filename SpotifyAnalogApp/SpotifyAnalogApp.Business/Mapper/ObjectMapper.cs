using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Data.Models;

namespace SpotifyAnalogApp.Business.Mapper
{
   public class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<SpotifyAnalogMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class SpotifyAnalogMapper : Profile
    {
        public SpotifyAnalogMapper()
        {
            CreateMap<Author, AuthorModel>().ReverseMap();
            CreateMap<Song, SongModel>().ReverseMap();
            CreateMap<Genre, GenreModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Playlist, PlaylistModel>().ReverseMap();
            CreateMap<ModifyUserModel, User>()
                .ForMember(mem => mem.Name, model => model.Condition(src => !string.IsNullOrWhiteSpace(src.Name)))
                .ForMember(mem => mem.Email, model => model.Condition(src => !string.IsNullOrWhiteSpace(src.Email)))
                .ForMember(mem => mem.FavoriteSongs, model => model.Condition(src => src.FavoriteSongs != null))
                .ForMember(mem => mem.UsersPlaylists, model => model.Condition(src => src.UsersPlaylists != null))
                .ForMember(mem => mem.UserId, model => model.Ignore())
                .ForMember(mem => mem.DateCreated, model => model.Ignore());




        }

    }
}

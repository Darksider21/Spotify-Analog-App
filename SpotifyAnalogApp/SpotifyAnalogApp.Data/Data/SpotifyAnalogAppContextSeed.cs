using Microsoft.EntityFrameworkCore;
using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Data
{
    public class SpotifyAnalogAppContextSeed
    {
        public static async Task SeedAsync(SpotifyAnalogAppContext context)
        {
             context.Database.Migrate();
             context.Database.EnsureCreated();

            //if (!context.Genres.Any())
            //{
            //    context.Genres.AddRange(GetPreconfiguredGenres());
            //    await context.SaveChangesAsync();
            //}

            //if (!context.Authors.Any())
            //{
            //    context.Authors.AddRange(GetPreconfiguredAuthors());
            //   await context.SaveChangesAsync();
            //}

            if (!context.Songs.Any() && !context.Authors.Any() && !context.Genres.Any())
            {
                context.Songs.AddRange(GetPredefinedSongs());
                await context.SaveChangesAsync();
            }
        }



        //private static IEnumerable<Genre> GetPreconfiguredGenres()
        //{
        //    return new List<Genre>()
        //    {
        //        new Genre() {GenreName = "Rock"},
        //        new Genre() {GenreName = "Metal"},
        //        new Genre() {GenreName = "Classical"},
        //        new Genre() {GenreName = "Pop"},
        //        new Genre() {GenreName = "J-pop"},
        //        new Genre() {GenreName = "Electronic"},
        //        new Genre() {GenreName = "Jazz"}
        //    };
        //}

        //private static IEnumerable<Author> GetPreconfiguredAuthors()
        //{
        //    return new List<Author>()
        //    {
        //        new Author() {Name = "Skillet", GenreId = 1},
        //        new Author() {Name = "Led Zeppelin", GenreId = 1},
        //        new Author() {Name = "Billy Tallent",GenreId = 1},
        //        new Author() {Name = "Linkin Park", GenreId = 1},
        //        new Author() {Name = "Nickelback", GenreId = 1},

        //        new Author() {Name = "Avenged Sevenfold", GenreId = 2},
        //        new Author() {Name = "Metallica",GenreId = 2},
        //        new Author() {Name = "Disturbed", GenreId = 2},
        //        new Author() {Name = "Slipknot", GenreId = 2},
        //        new Author() {Name = "System of a Down", GenreId = 2},

        //        new Author() {Name = "Wolfgang Mozar", GenreId = 3},
        //        new Author() {Name = "Ludwig van Beethoven", GenreId = 3},
        //        new Author() {Name = "Johann Sebastian Bach", GenreId = 3},
        //        new Author() {Name = "Pyotr Ilych Tchaikovsky", GenreId = 3},
        //        new Author() {Name = "Antonio Vivaldi", GenreId = 3},

        //        new Author() {Name = "Adele", GenreId = 4},
        //        new Author() {Name = "Beyonce", GenreId = 4},
        //        new Author() {Name = "Britney Spears", GenreId = 4},
        //        new Author() {Name = "Elton John", GenreId = 4},
        //        new Author() {Name = "Katy Perry", GenreId = 4},


        //        new Author() {Name = "Kalafina", GenreId = 5},
        //        new Author() {Name = "Sid ", GenreId = 5},
        //        new Author() {Name = "Flow", GenreId = 5},
        //        new Author() {Name = "Asian Kung-Fu Generation", GenreId = 5},
        //        new Author() {Name = "LiSA", GenreId = 5 },


        //        new Author() {Name = "The Prodigy", GenreId = 6},
        //        new Author() {Name = "Gorillaz", GenreId = 6},
        //        new Author() {Name = "New Order", GenreId = 6},
        //        new Author() {Name = "Depeche Mode", GenreId = 6},
        //        new Author() {Name = "M83", GenreId = 6},


        //        new Author() {Name = "BWB", GenreId = 7},
        //        new Author() {Name = "Fourplay", GenreId = 7},
        //        new Author() {Name = "Casa Loma Orchestra", GenreId = 7},
        //        new Author() {Name = "The bad Plus", GenreId = 7},
        //        new Author() {Name = "The Jazz Messengers", GenreId = 7}
        //    };
        //}


        private static IEnumerable<Song> GetPredefinedSongs()
        {
            var rock = new Genre() { GenreName = "Rock" };
            var skillet = new Author() { Name = "Skillet", Genre = rock };
            return new List<Song>()
            {
                new Song() {Name= "Whispers in the dark", Genre = rock , Author =skillet},
                new Song() {Name= "Hero", Genre = rock , Author =skillet},
                new Song() {Name= "Never Surrender", Genre = rock , Author =skillet},
                new Song() {Name= "The Last Night", Genre = rock , Author =skillet},
                new Song() {Name= "Monster", Genre = rock , Author =skillet},
                new Song() {Name= "Awake and alive", Genre = rock , Author =skillet},
                new Song() {Name= "Comatose", Genre = rock , Author =skillet},
                new Song() {Name= "Dear agony", Genre = rock , Author =skillet},
                new Song() {Name= "Fingernails", Genre = rock , Author =skillet},
                new Song() {Name= "Better than Drugs", Genre = rock , Author =skillet}

            };
        }
    }
}

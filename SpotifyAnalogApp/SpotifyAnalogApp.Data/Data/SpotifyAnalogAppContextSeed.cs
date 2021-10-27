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
            var metal = new Genre() { GenreName = "Meatal" };
            var classical = new Genre() { GenreName = "Classisal" };
            var pop = new Genre() { GenreName = "Pop" };
            var jpop = new Genre() { GenreName = "JPop" };
            var electronic = new Genre() { GenreName = "electronic" };
            var jazz = new Genre() { GenreName = "jazz" };
            //genres

            var skillet = new Author() { Name = "Skillet", Genre = rock };
            var ledzeppelin = new Author() { Name = "Led Zeppelin", Genre = rock };
            var billyTalent = new Author() { Name = "Billy Tallent", Genre = rock };
            var linkinpark = new Author() { Name = "Linkin Park", Genre = rock };
            var nickelback = new Author() { Name = "Nickelback", Genre = rock };
                 


            var avengedSevenfold = new Author() { Name = "Avenged Sevenfold", Genre = metal };
            var metallica = new Author() { Name = "Metallica", Genre = metal };
            var Disturbed = new Author() { Name = "Disturbed", Genre = metal };
            var Slipknot = new Author() { Name = "Slipknot", Genre = metal };
            var SystemofaDown = new Author() { Name = "System of a Down", Genre = metal };
                     

            var WolfgangMozart = new Author() { Name = "Wolfgang Mozart", Genre = classical };
            var LudwigvanBeethoven = new Author() { Name = "Ludwig van Beethoven", Genre = classical };
            var JohannSebastianBach = new Author() { Name = "Johann Sebastian Bach", Genre = classical };
            var PyotrIlychTchaikovsky = new Author() { Name = "Pyotr Ilych Tchaikovsky", Genre = classical };
            var AntonioVivaldi = new Author() { Name = "Antonio Vivaldi", Genre = classical };
                         


            var Adele = new Author() { Name = "Adele", Genre = pop };
            var BritneySpears = new Author() { Name = "Britney Spears", Genre = pop };
            var EltonJohn = new Author() { Name = "Elton John", Genre = pop };
            var Beyonce = new Author() { Name = "Beyonce", Genre = pop };
            var KatyPerry = new Author() { Name = "Katy Perry", Genre = pop };

                    

            var Kalafina = new Author() { Name = "Kalafina", Genre = jpop };
            var Sid = new Author() { Name = "Sid", Genre = jpop };
            var Flow = new Author() { Name = "Flow", Genre = jpop };
            var AsianKungFuGeneration = new Author() { Name = "Asian Kung Fu Generation", Genre = jpop };
            var LiSA = new Author() { Name = "LiSA", Genre = jpop };
                    


            var TheProdigy = new Author() { Name = "The Prodigy", Genre = electronic };
            var Gorillaz = new Author() { Name = "Gorillaz", Genre = electronic };
            var NewOrder = new Author() { Name = "New Order", Genre = electronic };
            var DepecheMode = new Author() { Name = "Depeche Mode", Genre = electronic };
            var M83 = new Author() { Name = "M83", Genre = electronic };
                            


            var  BWB = new Author() { Name = " BWB", Genre = jazz };
            var Fourplay = new Author() { Name = "Fourplay", Genre = jazz };
            var CasaLomaOrchestra = new Author() { Name = "Casa Loma Orchestra", Genre = jazz };
            var ThebadPlus = new Author() { Name = "The bad Plus", Genre = jazz };
            var TheJazzMessengers = new Author() { Name = "The Jazz Messengers", Genre = jazz };



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
                new Song() {Name= "Better than Drugs", Genre = rock , Author =skillet},
                

                new Song() {Name= "Whole Lotta Love", Genre = rock , Author =ledzeppelin},
                new Song() {Name= "Stairway to Heaven", Genre = rock , Author =ledzeppelin},
                new Song() {Name= "Black Dog", Genre = rock , Author =ledzeppelin},
                new Song() {Name= "Kashmir", Genre = rock , Author =ledzeppelin},
                new Song() {Name= "Ramble On", Genre = rock , Author =ledzeppelin},
                new Song() {Name= "Immigrant Song", Genre = rock , Author =ledzeppelin},
                new Song() {Name= "When the Levee Breaks", Genre = rock , Author =ledzeppelin},
                new Song() {Name= "Rock and Roll", Genre = rock , Author =ledzeppelin},
                new Song() {Name= "Misty Mountain Hop", Genre = rock , Author =ledzeppelin},
                new Song() {Name= "Good Times Bad Times", Genre = rock , Author =ledzeppelin},
                

                new Song() {Name= "Nothing To Lose", Genre = rock , Author =billyTalent},
                new Song() {Name= "Chasing The Sun", Genre = rock , Author =billyTalent},
                new Song() {Name= "Saint Veronika", Genre = rock , Author =billyTalent},
                new Song() {Name= "Perfect World", Genre = rock , Author =billyTalent},
                new Song() {Name= "Viking Death March", Genre = rock , Author =billyTalent},
                new Song() {Name= "Red Flag", Genre = rock , Author =billyTalent},
                new Song() {Name= "Line And Sinker", Genre = rock , Author =billyTalent},
                new Song() {Name= "The Crutch", Genre = rock , Author =billyTalent},
                new Song() {Name= "This Suffering", Genre = rock , Author =billyTalent},
                new Song() {Name= "Don't Count On The Wicked", Genre = rock , Author =billyTalent},


                new Song() {Name= "Numb", Genre = rock , Author =linkinpark},
                new Song() {Name= "In The End", Genre = rock , Author =linkinpark},
                new Song() {Name= "Faint", Genre = rock , Author =linkinpark},
                new Song() {Name= "One Step Closer", Genre = rock , Author =linkinpark},
                new Song() {Name= "Papercut", Genre = rock , Author =linkinpark},
                new Song() {Name= "One More Light", Genre = rock , Author =linkinpark},
                new Song() {Name= "Crawling", Genre = rock , Author =linkinpark},
                new Song() {Name= "Somewhere I Belong", Genre = rock , Author =linkinpark},
                new Song() {Name= "Wretches And Kings", Genre = rock , Author =linkinpark},
                new Song() {Name= "What I’ve Done", Genre = rock , Author =linkinpark},

                new Song() {Name= "Gotta Be Somebody", Genre = rock , Author =nickelback},
                new Song() {Name= "If Everyone Cared", Genre = rock , Author =nickelback},
                new Song() {Name= "Someday", Genre = rock , Author =nickelback},
                new Song() {Name= "Savin’ Me", Genre = rock , Author =nickelback},
                new Song() {Name= "When We Stand Together", Genre = rock , Author =nickelback},
                new Song() {Name= "If Today Was Your Last Day", Genre = rock , Author =nickelback},
                new Song() {Name= "Far Away", Genre = rock , Author =nickelback},
                new Song() {Name= "Rockstar", Genre = rock , Author =nickelback},
                new Song() {Name= "Photograph", Genre = rock , Author =nickelback},
                new Song() {Name= "How You Remind Me", Genre = rock , Author =nickelback},

                


                new Song() {Name= "Chapter Four", Genre = metal , Author =avengedSevenfold},
                new Song() {Name= "Seize the day", Genre = metal , Author =avengedSevenfold},
                new Song() {Name= "Bat Country", Genre = metal , Author =avengedSevenfold},
                new Song() {Name= "Second Hearthbeat", Genre = metal , Author =avengedSevenfold},
                new Song() {Name= "Danger line", Genre = metal , Author =avengedSevenfold},
                new Song() {Name= "Save me", Genre = metal , Author =avengedSevenfold},
                new Song() {Name= "Buried Alive", Genre = metal , Author =avengedSevenfold},
                new Song() {Name= "God Hates us", Genre = metal , Author =avengedSevenfold},
                new Song() {Name= "MIA", Genre = metal , Author =avengedSevenfold},
                new Song() {Name= "Nightmare", Genre = metal , Author =avengedSevenfold},
                
                new Song() {Name= "Master of Puppets", Genre = metal , Author =metallica},
                new Song() {Name= "Fight Fire With Fire", Genre = metal , Author =metallica},
                new Song() {Name= "Nothing Else Matters", Genre = metal , Author =metallica},
                new Song() {Name= "Unforgiven III", Genre = metal , Author =metallica},
                new Song() {Name= "Creeping Death", Genre = metal , Author =metallica},
                new Song() {Name= "One", Genre = metal , Author =metallica},
                new Song() {Name= "For Whom The Bell Tolls", Genre = metal , Author =metallica},
                new Song() {Name= "Battery", Genre = metal , Author =metallica},
                new Song() {Name= "Whiplash", Genre = metal , Author =metallica},
                new Song() {Name= "Fade to Black", Genre = metal , Author =metallica},
               
                new Song() {Name= " Decadance", Genre = metal , Author =Disturbed},
                new Song() {Name= "Innocense", Genre = metal , Author =Disturbed},
                new Song() {Name= "Light", Genre = metal , Author =Disturbed},
                new Song() {Name= "Inside the Fire", Genre = metal , Author =Disturbed},
                new Song() {Name= "Hell", Genre = metal , Author =Disturbed},
                new Song() {Name= "The Sound of Silence", Genre = metal , Author =Disturbed},
                new Song() {Name= "The Vengeful one", Genre = metal , Author =Disturbed},
                new Song() {Name= "You`re Mine", Genre = metal , Author =Disturbed},
                new Song() {Name= "Warrior", Genre = metal , Author =Disturbed},
                new Song() {Name= "The Night", Genre = metal , Author =Disturbed},
                
                new Song() {Name= "Psychosocial", Genre = metal , Author =Slipknot},
                new Song() {Name= "Snuff", Genre = metal , Author =Slipknot},
                new Song() {Name= "Duality", Genre = metal , Author =Slipknot},
                new Song() {Name= "Before I Forget", Genre = metal , Author =Slipknot},
                new Song() {Name= "Dead Memories", Genre = metal , Author =Slipknot},
                new Song() {Name= "People = Shit", Genre = metal , Author =Slipknot},
                new Song() {Name= "Wait and Bleed", Genre = metal , Author =Slipknot},
                new Song() {Name= "The Devil in I", Genre = metal , Author =Slipknot},
                new Song() {Name= "Spit It Out", Genre = metal , Author =Slipknot},
                new Song() {Name= "Disasterpiece", Genre = metal , Author =Slipknot},
                
                new Song() {Name= "Chop Suey!", Genre = metal , Author =SystemofaDown},
                new Song() {Name= "Aerials", Genre = metal , Author =SystemofaDown},
                new Song() {Name= "B.Y.O.B", Genre = metal , Author =SystemofaDown},
                new Song() {Name= "Lonely Day", Genre = metal , Author =SystemofaDown},
                new Song() {Name= "Toxisity", Genre = metal , Author =SystemofaDown},
                new Song() {Name= "Prison Song", Genre = metal , Author =SystemofaDown},
                new Song() {Name= "Sugar", Genre = metal , Author =SystemofaDown},
                new Song() {Name= "Holy Mountais", Genre = metal , Author =SystemofaDown},
                new Song() {Name= "Question!", Genre = metal , Author =SystemofaDown},
                new Song() {Name= "Radio/Video", Genre = metal , Author =SystemofaDown},



                

                new Song() {Name= "Requiem", Genre = classical , Author =WolfgangMozart},
                new Song() {Name= "The Magic Flite", Genre = classical , Author =WolfgangMozart},
                new Song() {Name= "Symphony No. 40", Genre = classical , Author =WolfgangMozart},
                new Song() {Name= "Eine kleine Nachtmusic", Genre = classical , Author =WolfgangMozart},
                new Song() {Name= "The Marrige of Figaro", Genre = classical , Author =WolfgangMozart},
                new Song() {Name= "Sonata for Two Pianos", Genre = classical , Author =WolfgangMozart},
                new Song() {Name= "Don Giovanni", Genre = classical , Author =WolfgangMozart},
                new Song() {Name= "Clarinet Concerto", Genre = classical , Author =WolfgangMozart},
                new Song() {Name= "Leck mich im Arsch", Genre = classical , Author =WolfgangMozart},
                new Song() {Name= "Ave verum corpus", Genre = classical , Author =WolfgangMozart},
                
                new Song() {Name= "Symphony No. 5", Genre = classical , Author =LudwigvanBeethoven},
                new Song() {Name= "Symphony No. 7", Genre = classical , Author =LudwigvanBeethoven},
                new Song() {Name= "Symphony No. 9", Genre = classical , Author =LudwigvanBeethoven},
                new Song() {Name= "Symphony No. 3", Genre = classical , Author =LudwigvanBeethoven},
                new Song() {Name= "Symphony No. 6", Genre = classical , Author =LudwigvanBeethoven},
                new Song() {Name= "Piano Sonata No.8", Genre = classical , Author =LudwigvanBeethoven},
                new Song() {Name= "Für Elise", Genre = classical , Author =LudwigvanBeethoven},
                new Song() {Name= "Violin Concerto", Genre = classical , Author =LudwigvanBeethoven},
                new Song() {Name= "Turkish March", Genre = classical , Author =LudwigvanBeethoven},
                new Song() {Name= "Missa solemnis", Genre = classical , Author =LudwigvanBeethoven},
                

                new Song() {Name= "Toccata and Fugue in D minor, BWV 565", Genre = classical , Author =JohannSebastianBach},
                new Song() {Name= "Air on the G Stringk", Genre = classical , Author =JohannSebastianBach},
                new Song() {Name= "Fugue in G minor, BWV 578", Genre = classical , Author =JohannSebastianBach},
                new Song() {Name= "Goldberg Variations", Genre = classical , Author =JohannSebastianBach},
                new Song() {Name= "St. Matthew Passion", Genre = classical , Author =JohannSebastianBach},
                new Song() {Name= "Inventions and Sinfonias", Genre = classical , Author =JohannSebastianBach},
                new Song() {Name= "Ave Maria", Genre = classical , Author =JohannSebastianBach},
                new Song() {Name= "Jesu, Joy of Man's Desiring", Genre = classical , Author =JohannSebastianBach},
                new Song() {Name= "Mass in B minor", Genre = classical , Author =JohannSebastianBach},
                new Song() {Name= "Concerto for Two Violins", Genre = classical , Author =JohannSebastianBach},
                

                new Song() {Name= "Piano Concerto No. 1", Genre = classical , Author =PyotrIlychTchaikovsky},
                new Song() {Name= "Swan Lake", Genre = classical , Author =PyotrIlychTchaikovsky},
                new Song() {Name= "1812 Overture", Genre = classical , Author =PyotrIlychTchaikovsky},
                new Song() {Name= "Violin Concerto", Genre = classical , Author =PyotrIlychTchaikovsky},
                new Song() {Name= "The Seasons", Genre = classical , Author =PyotrIlychTchaikovsky},
                new Song() {Name= "Romeo and Juliet", Genre = classical , Author =PyotrIlychTchaikovsky},
                new Song() {Name= "Serenade for Strings", Genre = classical , Author =PyotrIlychTchaikovsky},
                new Song() {Name= "Album pour enfants", Genre = classical , Author =PyotrIlychTchaikovsky},
                new Song() {Name= "Eugene Onegin", Genre = classical , Author =PyotrIlychTchaikovsky},
                new Song() {Name= "Dance of the Sugar Plum Fairy", Genre = classical , Author =PyotrIlychTchaikovsky},
                

                new Song() {Name= "The Four Seasons", Genre = classical , Author =AntonioVivaldi},
                new Song() {Name= "Gloria", Genre = classical , Author =AntonioVivaldi},
                new Song() {Name= "Stabat Mater", Genre = classical , Author =AntonioVivaldi},
                new Song() {Name= "La Gloria e Himeneo", Genre = classical , Author =AntonioVivaldi},
                new Song() {Name= "Mandolin Concerto", Genre = classical , Author =AntonioVivaldi},
                new Song() {Name= "Magnificat", Genre = classical , Author =AntonioVivaldi},
                new Song() {Name= "Orlando furioso", Genre = classical , Author =AntonioVivaldi},
                new Song() {Name= "La stravaganza", Genre = classical , Author =AntonioVivaldi},
                new Song() {Name= "Bajazet", Genre = classical , Author =AntonioVivaldi},
                new Song() {Name= "Giustino", Genre = classical , Author =AntonioVivaldi},




                new Song() {Name= "hello", Genre = pop , Author =Adele},
                new Song() {Name= "rolling in the deep", Genre = pop , Author =Adele},
                new Song() {Name= "someone like you", Genre = pop , Author =Adele},
                new Song() {Name= "set fire to the rain", Genre = pop , Author =Adele},
                new Song() {Name= "when we were young", Genre = pop , Author =Adele},
                new Song() {Name= "make you feel my love", Genre = pop , Author =Adele},
                new Song() {Name= "chasing pavements", Genre = pop , Author =Adele},
                new Song() {Name= "send my love", Genre = pop , Author =Adele},
                new Song() {Name= "love in the dark", Genre = pop , Author =Adele},
                new Song() {Name= "adele all i ask", Genre = pop , Author =Adele},
                

                new Song() {Name= "child bills, bills, bills", Genre = pop , Author =Beyonce},
                new Song() {Name= "halo", Genre = pop , Author =Beyonce},
                new Song() {Name= "crazy in love", Genre = pop , Author =Beyonce},
                new Song() {Name= "countdown", Genre = pop , Author =Beyonce},
                new Song() {Name= "single ladies", Genre = pop , Author =Beyonce},
                new Song() {Name= "run the world", Genre = pop , Author =Beyonce},
                new Song() {Name= "si yo fuera un chico", Genre = pop , Author =Beyonce},
                new Song() {Name= "love on top", Genre = pop , Author =Beyonce},
                new Song() {Name= "irreplaceable", Genre = pop , Author =Beyonce},
                new Song() {Name= "partition", Genre = pop , Author =Beyonce},
                

                new Song() {Name= "toxic", Genre = pop , Author =BritneySpears},
                new Song() {Name= "oops!...i did it again", Genre = pop , Author =BritneySpears},
                new Song() {Name= "criminal", Genre = pop , Author =BritneySpears},
                new Song() {Name= "gimme more", Genre = pop , Author =BritneySpears},
                new Song() {Name= "circus", Genre = pop , Author =BritneySpears},
                new Song() {Name= "...baby one more time", Genre = pop , Author =BritneySpears},
                new Song() {Name= "womanizer", Genre = pop , Author =BritneySpears},
                new Song() {Name= "if u seek amy", Genre = pop , Author =BritneySpears},
                new Song() {Name= "sometimes", Genre = pop , Author =BritneySpears},
                new Song() {Name= "i'm a slave 4 u", Genre = pop , Author =BritneySpears},
                

                new Song() {Name= "cold heart", Genre = pop , Author =EltonJohn},
                new Song() {Name= "your song", Genre = pop , Author =EltonJohn},
                new Song() {Name= "i'm still standing", Genre = pop , Author =EltonJohn},
                new Song() {Name= "tiny dancer", Genre = pop , Author =EltonJohn},
                new Song() {Name= "rocket man", Genre = pop , Author =EltonJohn},
                new Song() {Name= "sacrifice", Genre = pop , Author =EltonJohn},
                new Song() {Name= "bennie and the jets", Genre = pop , Author =EltonJohn},
                new Song() {Name= "don't go breaking my heart", Genre = pop , Author =EltonJohn},
                new Song() {Name= "candle in the wind", Genre = pop , Author =EltonJohn},
                new Song() {Name= "can you feel the love tonight", Genre = pop , Author =EltonJohn},
                

                new Song() {Name= "dark horse", Genre = pop , Author =KatyPerry},
                new Song() {Name= "roar", Genre = pop , Author =KatyPerry},
                new Song() {Name= "i kissed a girl", Genre = pop , Author =KatyPerry},
                new Song() {Name= "harleys in hawaii", Genre = pop , Author =KatyPerry},
                new Song() {Name= "hot n cold", Genre = pop , Author =KatyPerry},
                new Song() {Name= "con calma", Genre = pop , Author =KatyPerry},
                new Song() {Name= "the one that got away", Genre = pop , Author =KatyPerry},
                new Song() {Name= "last friday night", Genre = pop , Author =KatyPerry},
                new Song() {Name= "california gurls", Genre = pop , Author =KatyPerry},
                new Song() {Name= "bon appétit", Genre = pop , Author =KatyPerry},


                

                new Song() {Name= "to the beginning", Genre = jpop , Author =Kalafina},
                new Song() {Name= "magia", Genre = jpop , Author =Kalafina},
                new Song() {Name= "heavenly blue", Genre = jpop , Author =Kalafina},
                new Song() {Name= "oblivious", Genre = jpop , Author =Kalafina},
                new Song() {Name= "kimino ginno niwa", Genre = jpop , Author =Kalafina},
                new Song() {Name= "aria", Genre = jpop , Author =Kalafina},
                new Song() {Name= "kagayaku sorano shijimaniwa", Genre = jpop , Author =Kalafina},
                new Song() {Name= "manten", Genre = jpop , Author =Kalafina},
                new Song() {Name= "storia", Genre = jpop , Author =Kalafina},
                new Song() {Name= "ring your bell", Genre = jpop , Author =Kalafina},
                

                new Song() {Name= "ranbu no melody", Genre = jpop , Author =Sid},
                new Song() {Name= "uso", Genre = jpop , Author =Sid},
                new Song() {Name= "monokuro no kiss", Genre = jpop , Author =Sid},
                new Song() {Name= "enamel", Genre = jpop , Author =Sid},
                new Song() {Name= "delete", Genre = jpop , Author =Sid},
                new Song() {Name= "rain", Genre = jpop , Author =Sid},
                new Song() {Name= "glass no hitomi", Genre = jpop , Author =Sid},
                new Song() {Name= "star forest", Genre = jpop , Author =Sid},
                new Song() {Name= "yumegokochi", Genre = jpop , Author =Sid},
                new Song() {Name= "rasen no yume", Genre = jpop , Author =Sid},
                

                new Song() {Name= "Sign", Genre = jpop , Author =Flow},
                new Song() {Name= "kaze no uta", Genre = jpop , Author =Flow},
                new Song() {Name= "howling", Genre = jpop , Author =Flow},
                new Song() {Name= "re:member", Genre = jpop , Author =Flow},
                new Song() {Name= "go!!!", Genre = jpop , Author =Flow},
                new Song() {Name= "neiro", Genre = jpop , Author =Flow},
                new Song() {Name= "fuyu no amaoto", Genre = jpop , Author =Flow},
                new Song() {Name= "life is beautiful", Genre = jpop , Author =Flow},
                new Song() {Name= "blue bird", Genre = jpop , Author =Flow},
                new Song() {Name= "arigatou", Genre = jpop , Author =Flow},
                

                new Song() {Name= "rewrite", Genre = jpop , Author =AsianKungFuGeneration},
                new Song() {Name= "after dark", Genre = jpop , Author =AsianKungFuGeneration},
                new Song() {Name= "blood circulator", Genre = jpop , Author =AsianKungFuGeneration},
                new Song() {Name= "Re:Re", Genre = jpop , Author =AsianKungFuGeneration},
                new Song() {Name= "Dororo", Genre = jpop , Author =AsianKungFuGeneration},
                new Song() {Name= "Empathy", Genre = jpop , Author =AsianKungFuGeneration},

                
                new Song() {Name= "My Soul, Your Beats", Genre = jpop , Author =LiSA},
                 new Song() {Name= "Dawn", Genre = jpop , Author =LiSA},
                  new Song() {Name= "Ash", Genre = jpop , Author =LiSA},
                   new Song() {Name= "This Illusion", Genre = jpop , Author =LiSA},
                    new Song() {Name= "Gurenge", Genre = jpop , Author =LiSA},
                     new Song() {Name= "Rising hope", Genre = jpop , Author =LiSA},
                      new Song() {Name= "Rally Go Round", Genre = jpop , Author =LiSA},
                       new Song() {Name= "crossing field", Genre = jpop , Author =LiSA},
                        new Song() {Name= "Shirushi", Genre = jpop , Author =LiSA},


                        

                new Song() {Name= "firestarter", Genre = electronic , Author =TheProdigy},
                new Song() {Name= "smack my bitch up", Genre = electronic , Author =TheProdigy},
                new Song() {Name= "voodoo people", Genre = electronic , Author =TheProdigy},
                new Song() {Name= "breathe", Genre = electronic , Author =TheProdigy},
                new Song() {Name= "out of space", Genre = electronic , Author =TheProdigy},
                new Song() {Name= "no good", Genre = electronic , Author =TheProdigy},
                new Song() {Name= "break &enter", Genre = electronic , Author =TheProdigy},
                new Song() {Name= "warriors dance", Genre = electronic , Author =TheProdigy},
                new Song() {Name= "omen", Genre = electronic , Author =TheProdigy},
                new Song() {Name= "everybody in the place", Genre = electronic , Author =TheProdigy},
                

                new Song() {Name= "feel good inc", Genre = electronic , Author =Gorillaz},
                new Song() {Name= "rhinestone eyes", Genre = electronic , Author =Gorillaz},
                new Song() {Name= "clint eastwood", Genre = electronic , Author =Gorillaz},
                new Song() {Name= "she's my collar", Genre = electronic , Author =Gorillaz},
                new Song() {Name= "on melancholy hill", Genre = electronic , Author =Gorillaz},
                new Song() {Name= "dare", Genre = electronic , Author =Gorillaz},
                new Song() {Name= "dirty harry", Genre = electronic , Author =Gorillaz},
                new Song() {Name= "19-2000", Genre = electronic , Author =Gorillaz},
                new Song() {Name= "tranz", Genre = electronic , Author =Gorillaz},
                new Song() {Name= "saturnz barz", Genre = electronic , Author =Gorillaz},
                

                new Song() {Name= "blue monday '88 dub", Genre = electronic , Author =NewOrder},
                new Song() {Name= "bizarre love triangle", Genre = electronic , Author =NewOrder},
                new Song() {Name= "true faith", Genre = electronic , Author =NewOrder},
                new Song() {Name= "age of consent", Genre = electronic , Author =NewOrder},
                new Song() {Name= "the perfect kiss", Genre = electronic , Author =NewOrder},
                new Song() {Name= "temptation", Genre = electronic , Author =NewOrder},
                new Song() {Name= "love vigilantes", Genre = electronic , Author =NewOrder},
                new Song() {Name= "your silent face", Genre = electronic , Author =NewOrder},
                new Song() {Name= "world in motion", Genre = electronic , Author =NewOrder},
                new Song() {Name= "dreams never end", Genre = electronic , Author =NewOrder},
                

                new Song() {Name= "enjoy the silence", Genre = electronic , Author =DepecheMode},
                new Song() {Name= "personal jesus", Genre = electronic , Author =DepecheMode},
                new Song() {Name= "never let me down again", Genre = electronic , Author =DepecheMode},
                new Song() {Name= "just can't get enough", Genre = electronic , Author =DepecheMode},
                new Song() {Name= "strangelove", Genre = electronic , Author =DepecheMode},
                new Song() {Name= "policy of truth", Genre = electronic , Author =DepecheMode},
                new Song() {Name= "everything counts", Genre = electronic , Author =DepecheMode},
                new Song() {Name= "walking in my shoes", Genre = electronic , Author =DepecheMode},
                new Song() {Name= "people are people", Genre = electronic , Author =DepecheMode},
                new Song() {Name= "it's no good", Genre = electronic , Author =DepecheMode},
                

                new Song() {Name= "midnight city", Genre = electronic , Author =M83},
                new Song() {Name= "wait", Genre = electronic , Author =M83},
                new Song() {Name= "outro", Genre = electronic , Author =M83},
                new Song() {Name= "my tears are becoming a sea", Genre = electronic , Author =M83},
                new Song() {Name= "lower your eyelids to die with the sun", Genre = electronic , Author =M83},
                new Song() {Name= "oblivion", Genre = electronic , Author =M83},
                new Song() {Name= "we own the sky", Genre = electronic , Author =M83},
                new Song() {Name= "i need you", Genre = electronic , Author =M83},
                new Song() {Name= "kim & jessie", Genre = electronic , Author =M83},





                new Song() {Name= "billie jean", Genre = jazz , Author = BWB},
                new Song() {Name= "let's do it again", Genre = jazz , Author = BWB},
                new Song() {Name= "i can’t help it", Genre = jazz , Author = BWB},
                new Song() {Name= "groovin'", Genre = jazz , Author = BWB},
                new Song() {Name= "she’s out of my life", Genre = jazz , Author = BWB},
                new Song() {Name= "the way you make me feel", Genre = jazz , Author = BWB},
                new Song() {Name= "i want you girl", Genre = jazz , Author = BWB},
                new Song() {Name= "who’s lovin’ you", Genre = jazz , Author = BWB},
                new Song() {Name= "shake your body", Genre = jazz , Author = BWB},
                new Song() {Name= "man in the mirror", Genre = jazz , Author = BWB},
                

                new Song() {Name= "eastbound", Genre = jazz , Author = Fourplay},
                new Song() {Name= "after the dance", Genre = jazz , Author = Fourplay},
                new Song() {Name= "bali run", Genre = jazz , Author = Fourplay},
                new Song() {Name= "between the sheets", Genre = jazz , Author = Fourplay},
                new Song() {Name= "max-o-man", Genre = jazz , Author = Fourplay},
                new Song() {Name= "chant", Genre = jazz , Author = Fourplay},
                new Song() {Name= "foreplay", Genre = jazz , Author = Fourplay},
                new Song() {Name= "why can't it wait till morning", Genre = jazz , Author = Fourplay},
                new Song() {Name= "blues force", Genre = jazz , Author = Fourplay},
                new Song() {Name= "4 play and pleasure", Genre = jazz , Author = Fourplay},
                

                new Song() {Name= "happy days are here again", Genre = jazz , Author = CasaLomaOrchestra},
                new Song() {Name= "smoke rings", Genre = jazz , Author = CasaLomaOrchestra},
                new Song() {Name= "my heart tells me", Genre = jazz , Author = CasaLomaOrchestra},
                new Song() {Name= "washboard blues", Genre = jazz , Author = CasaLomaOrchestra},
                new Song() {Name= "casa loma stomp", Genre = jazz , Author = CasaLomaOrchestra},
                new Song() {Name= "alexander's ragtime band", Genre = jazz , Author = CasaLomaOrchestra},
                new Song() {Name= "girl of my dreams", Genre = jazz , Author = CasaLomaOrchestra},
                new Song() {Name= "it's the talk of the town", Genre = jazz , Author = CasaLomaOrchestra},
                new Song() {Name= "in the mood", Genre = jazz , Author = CasaLomaOrchestra},
                new Song() {Name= "song of india", Genre = jazz , Author = CasaLomaOrchestra},
                

                new Song() {Name= "everybody wants to rule the world", Genre = jazz , Author = ThebadPlus},
                new Song() {Name= "flim", Genre = jazz , Author = ThebadPlus},
                new Song() {Name= "physical cities", Genre = jazz , Author = ThebadPlus},
                new Song() {Name= "hurricane birds", Genre = jazz , Author = ThebadPlus},
                new Song() {Name= "tom sawyer", Genre = jazz , Author = ThebadPlus},
                new Song() {Name= "boffadem", Genre = jazz , Author = ThebadPlus},
                new Song() {Name= "seven minute mind", Genre = jazz , Author = ThebadPlus},
                new Song() {Name= "life on mars", Genre = jazz , Author = ThebadPlus},
                new Song() {Name= "pound for pound", Genre = jazz , Author = ThebadPlus},
                new Song() {Name= "1972 bronze medalist", Genre = jazz , Author = ThebadPlus},
                

                new Song() {Name= "moanin'", Genre = jazz , Author = TheJazzMessengers},
                new Song() {Name= "dat dere", Genre = jazz , Author = TheJazzMessengers},
                new Song() {Name= "along came betty", Genre = jazz , Author = TheJazzMessengers},
                new Song() {Name= "close your eyes", Genre = jazz , Author = TheJazzMessengers},
                new Song() {Name= "hipsippy blues", Genre = jazz , Author = TheJazzMessengers},
                new Song() {Name= "jazz", Genre = jazz , Author = TheJazzMessengers},
                new Song() {Name= "1978", Genre = jazz , Author = TheJazzMessengers},
                new Song() {Name= "jimerick", Genre = jazz , Author = TheJazzMessengers},
                new Song() {Name= "no problem", Genre = jazz , Author = TheJazzMessengers},

            };
        }
    }
}

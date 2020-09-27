using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using RockStars.Comparators;
using RockStars.Modals;

namespace RockStars.DbContexts
{
    public class DbInitializer
    {
        public static void Initialize(MusicContext context)
        {
            // Check if database exists
            context.Database.EnsureCreated();

            // Look for any artists.
            if (context.Artists.Any())
            {
                return;   // DB has been seeded
            }

            string artistsJsonString = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Json", "artists.json"));
            List<Artist> artists = JsonSerializer.Deserialize<List<Artist>>(artistsJsonString);
            IEnumerable<Artist> filteredArtists = artists.Distinct();

            context.Artists.AddRange(filteredArtists);
            context.SaveChanges();

            string songsJsonString = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Json", "songs.json"));
            List<Song> songs = JsonSerializer.Deserialize<List<Song>>(songsJsonString);
            IEnumerable<Song> filteredSongs = songs
                .Distinct(new SongComparator())
                .Where(song => song.Genre.Contains("Metal") && song.Year < 2016);

            context.Songs.AddRange(filteredSongs);
            context.SaveChanges();
        }
    }
}

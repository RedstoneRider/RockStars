using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RockStars.Modals
{
    public class Song
    {
        public int ID { get; set; }

		public string Artist { get; set; }

		public string Name { get; set; }

		[JsonPropertyName("Shortname")]
		public string ShortName { get; set; }

		public int? Year { get; set; }	

		public int? Duration { get; set; } // Milliseconds

		public string Genre { get; set; }

		public int? Bpm { get; set; }

		public string SpotifyId { get; set; }

		public string Album { get; set; }
	}
}
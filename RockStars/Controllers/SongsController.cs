using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RockStars.DbContexts;
using RockStars.Modals;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RockStars.Controllers
{
    [Route("api/[controller]")]
    public class SongsController : Controller
    {
        private readonly MusicContext musicContext;

        public SongsController(MusicContext musicContext)
        {
            this.musicContext = musicContext;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get([FromQuery] string genre = "")
        {
            try
            {
                if (genre.Length > 0)
                {
                    return Ok(musicContext.Songs.Where(s => s.Genre == genre));
                }
                return Ok(musicContext.Songs.ToArray());
            }
            catch
            {
                return Problem("Could not respond to request");
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(musicContext.Songs.Find(id));
            }
            catch
            {
                return ValidationProblem(string.Format("Unable to find song with id: {0}", id));
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Song song)
        {
            try
            {
                musicContext.Songs.Add(song);
                musicContext.SaveChanges();
                return Ok(song);
            }
            catch
            {
                return ValidationProblem("Unable to add the new song");
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Song newSong)
        {
            try
            {
                var song = musicContext.Songs.Find(id);
                newSong.ID = song.ID;

                musicContext.Entry(song).CurrentValues.SetValues(newSong);
                musicContext.SaveChanges();

                return Ok(song);
            }
            catch
            {
                return ValidationProblem(string.Format("Unable to update the song with id: {0}", id));
            }    
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var song = musicContext.Songs.Find(id);
                musicContext.Songs.Remove(song);
                musicContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return ValidationProblem(string.Format("Unable to delete the song with id: {0}", id));
            }
        }
    }
}

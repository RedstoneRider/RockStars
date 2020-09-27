using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RockStars.DbContexts;
using RockStars.Modals;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RockStars.Controllers
{
    [Route("api/[controller]")]
    public class ArtistsController : Controller
    {
        private readonly MusicContext musicContext;

        public ArtistsController(MusicContext musicContext)
        {
            this.musicContext = musicContext;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get([FromQuery] string name = "")
        {
            try
            {
                if (name.Length > 0)
                {
                    return Ok(musicContext.Artists.Where(a => a.Name == name));
                }
                return Ok(musicContext.Artists.ToArray());
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
                return Ok(musicContext.Artists.Find(id));
            }
            catch
            {
                return ValidationProblem(string.Format("Unable to find artist with id: {0}", id));
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Artist artist)
        {
            try
            {
                musicContext.Artists.Add(artist);
                musicContext.SaveChanges();
                return Ok(artist);
            }
            catch
            {
                return ValidationProblem("Unable to add the new artist");
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Artist newArtist)
        {
            try
            {
                var artist = musicContext.Artists.Find(id);
                newArtist.ID = artist.ID;

                musicContext.Entry(artist).CurrentValues.SetValues(newArtist);
                musicContext.SaveChanges();

                return Ok(artist);
            }
            catch
            {
                return ValidationProblem(string.Format("Unable to update the artist with id: {0}", id));
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var artist = musicContext.Artists.Find(id);
                musicContext.Remove(artist);
                musicContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return ValidationProblem(string.Format("Unable to delete the artist with id: {0}", id));
            }
        }
    }
}

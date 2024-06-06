using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcRatings.Models;
using MvcRatings.Data;

namespace MvcRatings.Controllers
{
    // [Route("api/[controller]")]
    // [ApiController]
    public class SongController : Controller
    {
        private readonly ContextDb _context;

        public SongController(ContextDb context)
        {
            _context = context;
        }
        // GET: Song/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        
        // POST: Song/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            var songs = await _context.Song
                .Include(s => s.Artist) 
                .Where(s => s.Title.Contains(SearchPhrase))
                .ToListAsync();
            return PartialView("_SearchResults", songs);
        }
        public ActionResult AddSong(int id)
        {
            Album al = _context.Album.Where(a => a.Id == id).ToList().First();
            return View(al);
        }
        
        public async Task<ActionResult<Album>> SaveSong(Song model)
        {
            try
            {
                Song s = new Song();
                s.Title = model.Title;
                s.ReleaseDate = model.ReleaseDate;
                s.ArtistId = model.ArtistId;
                s.AlbumId = model.AlbumId;
                s.Duration = model.Duration;
                
                _context.Song.Add(s);
                _context.SaveChanges();
                return RedirectToAction("DetailsAlbum","Album", model.AlbumId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        
        // GET: api/Song
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSong()
        {
          if (_context.Song == null)
          {
              return NotFound();
          }
            return await _context.Song.ToListAsync();
        }

        // GET: api/Song/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
          if (_context.Song == null)
          {
              return NotFound();
          }
            var song = await _context.Song.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        // PUT: api/Song/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, Song song)
        {
            if (id != song.Id)
            {
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Song
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
          if (_context.Song == null)
          {
              return Problem("Entity set 'ContextDb.Song'  is null.");
          }
            _context.Song.Add(song);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSong", new { id = song.Id }, song);
        }

        // DELETE: api/Song/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            if (_context.Song == null)
            {
                return NotFound();
            }
            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Song.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(int id)
        {
            return (_context.Song?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

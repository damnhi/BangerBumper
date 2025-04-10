using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcRatings.Models;
using MvcRatings.Data;
using SQLitePCL;


namespace MvcRatings.Controllers
{
    public class Asongs
    {
        public List<Album> al {get;set;}
        public List<Song> sos {get;set;}
        public Asongs()
        {
            this.al = new List<Album>();
            this.sos = new List<Song>();
        }
    }
    public class AlbumController : Controller
    {
        private readonly ContextDb _context;

        public AlbumController(ContextDb context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> AlbumTestsView()
        {
            if (_context.Album == null)
            {
                return Problem("Entity set 'BangerBumper.Album'  is null.");
            }

            var albums = await _context.Album
                .Include(a => a.Artist) 
                .ToListAsync();

            return View(albums);
        }
        public async Task<IActionResult> DetailsAlbum(int Id)
        {
            if (_context.Album == null)
            {
                return Problem("Entity set 'BangerBumper.Album'  is null.");
            }

            Asongs ass = new Asongs();
            
            ass.al = _context.Album.Where(a => a.Id == Id).Include(o=>o.Artist).ToList();
            ass.sos = await _context.Song.Include(r => r.Album).Where(r => r.Album.Id == Id).Include(o=>o.Artist).ToListAsync();
            return View(ass);
        }
        
        public async Task<IActionResult> AlbumHome()
        {
            if (_context.Album == null)
            {
                return Problem("Entity set 'BangerBumper.Album'  is null.");
            }

            var albums = await _context.Album
                .Include(a => a.Artist).OrderByDescending(o=>o.ReleaseDate) // Ładowanie informacji o artyście dla każdego albumu
                .ToListAsync();

            return View(albums);
        }
        [Authorize]
        public ActionResult AddAlbum()
        {
            return View();
        }
        
        public async Task<ActionResult<Album>> SaveAlbum(Album model)
        {
            try
            {
                Album a = new Album();
                a.Title = model.Title;
                a.ReleaseDate = model.ReleaseDate;
                a.ArtistId = model.ArtistId;
                _context.Album.Add(a);
                _context.SaveChanges();
                return RedirectToAction("AlbumTestsView");
            }

            catch (Exception ex)
            {
                throw ex;

            }
        }
        [Authorize]
        public ActionResult EditAlbumView(int Id)
        {
            var std = _context.Album.Where(s => s.Id == Id).FirstOrDefault();
    
            return View(std);
        }
        [Authorize]
        public ActionResult EditAlbum(Album std)
        {
            var album = _context.Album.Where(s => s.Id == std.Id).FirstOrDefault();
            
            _context.Album.Remove(album);
            _context.Album.Add(std);
            _context.SaveChanges();

            return RedirectToAction("AlbumTestsView");
        }
        [Authorize]
        public async Task<IActionResult> RemoveAlbum(bool confirm, int id)
        {
            if (confirm)
            {
                if (_context.Album == null)
                {
                    return NotFound();
                }

                var album = await _context.Album.FindAsync(id);
                if (album == null)
                {
                    return NotFound();
                }
                
                _context.Album.Remove(album);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("AlbumTestsView");
        }
        
        //Query for API
        
        [Route("api/[controller]")]
        // GET: api/Album
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbum()
        {
          if (_context.Album == null)
          {
              return NotFound();
          }
          
          return await _context.Album.ToListAsync();
        }

        // GET: api/Album/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Album>> GetAlbum(int id)
        {
          if (_context.Album == null)
          {
              return NotFound();
          }
            var album = await _context.Album.FindAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            return album;
        }
        
        // PUT: api/Album/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, Album album)
        {
            if (id != album.Id)
            {
                return BadRequest();
            }

            _context.Entry(album).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(id))
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

        // POST: api/Album
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Album>> PostAlbum(Album album)
        {
          if (_context.Album == null)
          {
              return Problem("Entity set 'ContextDb.Album'  is null.");
          }
            _context.Album.Add(album);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlbum", new { id = album.Id }, album);
        }

        // DELETE: api/Album/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            if (_context.Album == null)
            {
                return NotFound();
            }
            var album = await _context.Album.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            _context.Album.Remove(album);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlbumExists(int id)
        {
            return (_context.Album?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

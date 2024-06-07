using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class RatingMaker
    {
        public Song so {get;set;} = new();
        public Guid uid {get;set;} = new();
        public DateTime date {get;set;} = new();
        public int val { get; set; } = new();
        public string com { get; set; }
    }
    public class RatingController : Controller
    {
        private readonly ContextDb _context;

        public RatingController(ContextDb context)
        {
            _context = context;
        }
        
        public ActionResult AddRating(int id)
        {   
            var lol = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            foreach (Rating r in _context.Rating.Where(r => r.SongId==id))
            {
                if (lol == r.UserId)
                {
                    return RedirectToAction("DetailsSong", "Song", new { id = id });
                }
            }
            RatingMaker rm = new RatingMaker();
            
            rm.uid = lol;
            rm.date = DateTime.Now;
            rm.so = _context.Song.Where(a => a.Id == id).ToList().First();
            return View(rm);
        }
        
        public async Task<ActionResult<Album>> SaveRating(RatingMaker model)
        {
            try
            {
                Rating r = new Rating();
                r.Value = model.val;
                r.Comment = model.com;
                r.SongId = model.so.Id;
                r.UserId = model.uid;
                r.Date = model.date;
                
                _context.Rating.Add(r);
                _context.SaveChanges();
                return RedirectToAction("DetailsSong","Song", new { id = model.so.Id});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        
        // GET: api/Rating
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRating()
        {
            /*
            var xd = _context.Rating.Include(r => r.Song).ToList().First().Song; |teraz działa bez include nie działa

            var aa = _context.Rating.Where(r => r.Song.Title == "asdasd")  |działa  .ToList().First().Song; |nie działa potrzebuje include
            */
            
          if (_context.Rating == null)
          {
              return NotFound();
          }
            return await _context.Rating.ToListAsync();
        }

        // GET: api/Rating/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRating(int id)
        {
          if (_context.Rating == null)
          {
              return NotFound();
          }
            var rating = await _context.Rating.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return rating;
        }

        // PUT: api/Rating/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(int id, Rating rating)
        {
            if (id != rating.Id)
            {
                return BadRequest();
            }

            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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

        // POST: api/Rating
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(Rating rating)
        {
          if (_context.Rating == null)
          {
              return Problem("Entity set 'ContextDb.Rating'  is null.");
          }
            _context.Rating.Add(rating);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRating", new { id = rating.Id }, rating);
        }

        // DELETE: api/Rating/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            if (_context.Rating == null)
            {
                return NotFound();
            }
            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingExists(int id)
        {
            return (_context.Rating?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

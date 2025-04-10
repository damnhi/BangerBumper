using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcRatings.Models;
using MvcRatings.Data;
using Microsoft.AspNetCore.Authorization;

namespace MvcRatings.Controllers
{
    
    public class UserController : Controller
    {
        private readonly ContextDb _context;

        public UserController(ContextDb context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> ShowUsers()
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'BangerBumper.User'  is null.");
            }
            
            var users = from u in _context.User
                select u;

            return View(await users.ToListAsync());
        }
        [Authorize]
        public async Task<IActionResult> RemoveUser(bool confirm, Guid id)
        {
            if (confirm)
            {
                if (_context.User == null)
                {
                    return NotFound();
                }

                var us = await _context.User.FindAsync(id);
                if (us == null)
                {
                    return NotFound();
                }
                
                _context.User.Remove(us);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("ShowUsers");
        }
        
        //Query for API
        
        [Route("api/[controller]")]
        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            return await _context.User.ToListAsync();
        }
        
        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (Guid.Parse(id)!= user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.User == null)
          {
              return Problem("Entity set 'ContextDb.User'  is null.");
          }
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return (_context.User?.Any(e => e.Id == Guid.Parse(id))).GetValueOrDefault();
        }
    }
}

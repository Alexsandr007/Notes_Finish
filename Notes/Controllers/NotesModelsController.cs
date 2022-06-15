using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Models;

namespace Notes.Controllers
{
    public class NotesModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NotesModels
        [Authorize]
        public async Task<IActionResult> Index()
        {

            ViewData["username"] = HttpContext.User.Identity.Name;
            return View(await _context.Movie.ToListAsync());
        }

        // GET: NotesModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notesModels = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notesModels == null)
            {
                return NotFound();
            }

            return View(notesModels);
        }

        // GET: NotesModels/Create
        public IActionResult Create()
        {
            ViewData["username"] = HttpContext.User.Identity.Name;
            return View();
        }

        // POST: NotesModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,user,ReleaseDate,Text")] NotesModels notesModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notesModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notesModels);
        }

        // GET: NotesModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notesModels = await _context.Movie.FindAsync(id);
            if (notesModels == null)
            {
                return NotFound();
            }
            return View(notesModels);
        }

        // POST: NotesModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,user,ReleaseDate,Text")] NotesModels notesModels)
        {
            if (id != notesModels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notesModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotesModelsExists(notesModels.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(notesModels);
        }

        // GET: NotesModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notesModels = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notesModels == null)
            {
                return NotFound();
            }

            return View(notesModels);
        }

        // POST: NotesModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notesModels = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(notesModels);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotesModelsExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}

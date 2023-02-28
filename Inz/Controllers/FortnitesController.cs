using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inz.Data;

namespace Inz.Controllers
{
    public class FortnitesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FortnitesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fortnites
        public async Task<IActionResult> Index(string SearchString)
        {
            //return View(await _context.Cs.ToListAsync());
            ViewData["CurrentFilter"] = SearchString;
            var Fortnite = from c in _context.Fortnite
                     select c;
            if (!String.IsNullOrEmpty(SearchString))
            {
                Fortnite = Fortnite.Where(c => c.Name.Contains(SearchString));
            }
            return View(Fortnite);
        }

        // GET: Fortnites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fortnite = await _context.Fortnite
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fortnite == null)
            {
                return NotFound();
            }

            return View(fortnite);
        }

        // GET: Fortnites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fortnites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Contact,Description")] Fortnite fortnite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fortnite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fortnite);
        }

        // GET: Fortnites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fortnite = await _context.Fortnite.FindAsync(id);
            if (fortnite == null)
            {
                return NotFound();
            }
            return View(fortnite);
        }

        // POST: Fortnites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Contact,Description")] Fortnite fortnite)
        {
            if (id != fortnite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fortnite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FortniteExists(fortnite.Id))
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
            return View(fortnite);
        }

        // GET: Fortnites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fortnite = await _context.Fortnite
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fortnite == null)
            {
                return NotFound();
            }

            return View(fortnite);
        }

        // POST: Fortnites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fortnite = await _context.Fortnite.FindAsync(id);
            _context.Fortnite.Remove(fortnite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FortniteExists(int id)
        {
            return _context.Fortnite.Any(e => e.Id == id);
        }
    }
}

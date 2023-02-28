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
    public class LolsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lols
        public async Task<IActionResult> Index(string SearchString)
        {
            //return View(await _context.Cs.ToListAsync());
            ViewData["CurrentFilter"] = SearchString;
            var Lol = from c in _context.Lol
                      select c;
            if (!String.IsNullOrEmpty(SearchString))
            {
                Lol = Lol.Where(c => c.Name.Contains(SearchString));
            }
            return View(Lol);
        }

        // GET: Lols/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lol = await _context.Lol
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lol == null)
            {
                return NotFound();
            }

            return View(lol);
        }

        // GET: Lols/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lols/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Contact,Description")] Lol lol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lol);
        }

        // GET: Lols/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lol = await _context.Lol.FindAsync(id);
            if (lol == null)
            {
                return NotFound();
            }
            return View(lol);
        }

        // POST: Lols/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Contact,Description")] Lol lol)
        {
            if (id != lol.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LolExists(lol.Id))
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
            return View(lol);
        }

        // GET: Lols/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lol = await _context.Lol
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lol == null)
            {
                return NotFound();
            }

            return View(lol);
        }

        // POST: Lols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lol = await _context.Lol.FindAsync(id);
            _context.Lol.Remove(lol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LolExists(int id)
        {
            return _context.Lol.Any(e => e.Id == id);
        }
    }
}

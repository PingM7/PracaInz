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
    public class SiatkowkasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SiatkowkasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Siatkowkas
        public async Task<IActionResult> Index(string SearchString)
        {
            //return View(await _context.Cs.ToListAsync());
            ViewData["CurrentFilter"] = SearchString;
            var Siatkowka = from c in _context.Siatkowka
                            select c;
            if (!String.IsNullOrEmpty(SearchString))
            {
                Siatkowka = Siatkowka.Where(c => c.Name.Contains(SearchString));
            }
            return View(Siatkowka);
        }

        // GET: Siatkowkas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siatkowka = await _context.Siatkowka
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siatkowka == null)
            {
                return NotFound();
            }

            return View(siatkowka);
        }

        // GET: Siatkowkas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Siatkowkas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Contact,Description")] Siatkowka siatkowka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siatkowka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(siatkowka);
        }

        // GET: Siatkowkas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siatkowka = await _context.Siatkowka.FindAsync(id);
            if (siatkowka == null)
            {
                return NotFound();
            }
            return View(siatkowka);
        }

        // POST: Siatkowkas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Contact,Description")] Siatkowka siatkowka)
        {
            if (id != siatkowka.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siatkowka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiatkowkaExists(siatkowka.Id))
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
            return View(siatkowka);
        }

        // GET: Siatkowkas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siatkowka = await _context.Siatkowka
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siatkowka == null)
            {
                return NotFound();
            }

            return View(siatkowka);
        }

        // POST: Siatkowkas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siatkowka = await _context.Siatkowka.FindAsync(id);
            _context.Siatkowka.Remove(siatkowka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiatkowkaExists(int id)
        {
            return _context.Siatkowka.Any(e => e.Id == id);
        }
    }
}

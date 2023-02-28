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
    public class KoszykowkasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KoszykowkasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Koszykowkas
        public async Task<IActionResult> Index(string SearchString)
        {
            //return View(await _context.Cs.ToListAsync());
            ViewData["CurrentFilter"] = SearchString;
            var Koszykowka = from c in _context.Koszykowka
                           select c;
            if (!String.IsNullOrEmpty(SearchString))
            {
                Koszykowka = Koszykowka.Where(c => c.Name.Contains(SearchString));
            }
            return View(Koszykowka);
        }

        // GET: Koszykowkas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var koszykowka = await _context.Koszykowka
                .FirstOrDefaultAsync(m => m.Id == id);
            if (koszykowka == null)
            {
                return NotFound();
            }

            return View(koszykowka);
        }

        // GET: Koszykowkas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Koszykowkas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Contact,Description")] Koszykowka koszykowka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(koszykowka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(koszykowka);
        }

        // GET: Koszykowkas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var koszykowka = await _context.Koszykowka.FindAsync(id);
            if (koszykowka == null)
            {
                return NotFound();
            }
            return View(koszykowka);
        }

        // POST: Koszykowkas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Contact,Description")] Koszykowka koszykowka)
        {
            if (id != koszykowka.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(koszykowka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KoszykowkaExists(koszykowka.Id))
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
            return View(koszykowka);
        }

        // GET: Koszykowkas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var koszykowka = await _context.Koszykowka
                .FirstOrDefaultAsync(m => m.Id == id);
            if (koszykowka == null)
            {
                return NotFound();
            }

            return View(koszykowka);
        }

        // POST: Koszykowkas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var koszykowka = await _context.Koszykowka.FindAsync(id);
            _context.Koszykowka.Remove(koszykowka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KoszykowkaExists(int id)
        {
            return _context.Koszykowka.Any(e => e.Id == id);
        }
    }
}

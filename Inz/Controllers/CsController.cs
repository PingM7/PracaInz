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
    public class CsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cs
        public async Task<IActionResult> Index(string SearchString)
        {
            //return View(await _context.Cs.ToListAsync());
            ViewData["CurrentFilter"] = SearchString;
            var cs = from c in _context.Cs
                     select c;
            if (!String.IsNullOrEmpty(SearchString))
            {
                cs = cs.Where(c => c.Name.Contains(SearchString));
            }
            return View(cs);
        }

        // GET: Cs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cs = await _context.Cs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cs == null)
            {
                return NotFound();
            }

            return View(cs);
        }

        // GET: Cs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Contact,Description")] Cs cs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cs);
        }

        // GET: Cs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cs = await _context.Cs.FindAsync(id);
            if (cs == null)
            {
                return NotFound();
            }
            return View(cs);
        }

        // POST: Cs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Contact,Description")] Cs cs)
        {
            if (id != cs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CsExists(cs.Id))
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
            return View(cs);
        }

        // GET: Cs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cs = await _context.Cs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cs == null)
            {
                return NotFound();
            }

            return View(cs);
        }

        // POST: Cs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cs = await _context.Cs.FindAsync(id);
            _context.Cs.Remove(cs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CsExists(int id)
        {
            return _context.Cs.Any(e => e.Id == id);
        }
    }
}

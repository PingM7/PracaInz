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
    public class PilkaNoznasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PilkaNoznasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PilkaNoznas
        public async Task<IActionResult> Index(string SearchString)
        {
            //return View(await _context.Cs.ToListAsync());
            ViewData["CurrentFilter"] = SearchString;
            var PilkaNozna = from c in _context.PilkaNozna
                             select c;
            if (!String.IsNullOrEmpty(SearchString))
            {
                PilkaNozna = PilkaNozna.Where(c => c.Name.Contains(SearchString));
            }
            return View(PilkaNozna);
        }

        // GET: PilkaNoznas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pilkaNozna = await _context.PilkaNozna
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pilkaNozna == null)
            {
                return NotFound();
            }

            return View(pilkaNozna);
        }

        // GET: PilkaNoznas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PilkaNoznas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Contact,Description")] PilkaNozna pilkaNozna)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pilkaNozna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pilkaNozna);
        }

        // GET: PilkaNoznas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pilkaNozna = await _context.PilkaNozna.FindAsync(id);
            if (pilkaNozna == null)
            {
                return NotFound();
            }
            return View(pilkaNozna);
        }

        // POST: PilkaNoznas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Contact,Description")] PilkaNozna pilkaNozna)
        {
            if (id != pilkaNozna.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pilkaNozna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PilkaNoznaExists(pilkaNozna.Id))
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
            return View(pilkaNozna);
        }

        // GET: PilkaNoznas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pilkaNozna = await _context.PilkaNozna
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pilkaNozna == null)
            {
                return NotFound();
            }

            return View(pilkaNozna);
        }

        // POST: PilkaNoznas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pilkaNozna = await _context.PilkaNozna.FindAsync(id);
            _context.PilkaNozna.Remove(pilkaNozna);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PilkaNoznaExists(int id)
        {
            return _context.PilkaNozna.Any(e => e.Id == id);
        }
    }
}

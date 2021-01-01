using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blogum2.Data;
using Blogum2.Models;

namespace Blogum2.Controllers
{
    public class IlController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IlController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Il
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Il.Include(i => i.Ilce).Include(i => i.Tur);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Il/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var il = await _context.Il
                .Include(i => i.Ilce)
                .Include(i => i.Tur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (il == null)
            {
                return NotFound();
            }

            return View(il);
        }

        // GET: Il/Create
        public IActionResult Create()
        {
            ViewData["IlceId"] = new SelectList(_context.Ilce, "Id", "IlceAd");
            ViewData["TurId"] = new SelectList(_context.Tur, "Id", "GeziTuru");
            return View();
        }

        // POST: Il/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IlAd,TurId,IlceId")] Il il)
        {
            if (ModelState.IsValid)
            {
                _context.Add(il);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IlceId"] = new SelectList(_context.Ilce, "Id", "IlceAd", il.IlceId);
            ViewData["TurId"] = new SelectList(_context.Tur, "Id", "GeziTuru", il.TurId);
            return View(il);
        }

        // GET: Il/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var il = await _context.Il.FindAsync(id);
            if (il == null)
            {
                return NotFound();
            }
            ViewData["IlceId"] = new SelectList(_context.Ilce, "Id", "IlceAd", il.IlceId);
            ViewData["TurId"] = new SelectList(_context.Tur, "Id", "GeziTuru", il.TurId);
            return View(il);
        }

        // POST: Il/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IlAd,TurId,IlceId")] Il il)
        {
            if (id != il.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(il);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IlExists(il.Id))
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
            ViewData["IlceId"] = new SelectList(_context.Ilce, "Id", "IlceAd", il.IlceId);
            ViewData["TurId"] = new SelectList(_context.Tur, "Id", "GeziTuru", il.TurId);
            return View(il);
        }

        // GET: Il/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var il = await _context.Il
                .Include(i => i.Ilce)
                .Include(i => i.Tur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (il == null)
            {
                return NotFound();
            }

            return View(il);
        }

        // POST: Il/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var il = await _context.Il.FindAsync(id);
            _context.Il.Remove(il);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IlExists(int id)
        {
            return _context.Il.Any(e => e.Id == id);
        }
    }
}

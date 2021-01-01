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
    public class IlSehirYeriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IlSehirYeriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IlSehirYeri
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IlSehirYeri.Include(i => i.Il).Include(i => i.SehirdekiYer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: IlSehirYeri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilSehirYeri = await _context.IlSehirYeri
                .Include(i => i.Il)
                .Include(i => i.SehirdekiYer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilSehirYeri == null)
            {
                return NotFound();
            }

            return View(ilSehirYeri);
        }

        // GET: IlSehirYeri/Create
        public IActionResult Create()
        {
            ViewData["IlId"] = new SelectList(_context.Il, "Id", "IlAd");
            ViewData["SehirdekiYerId"] = new SelectList(_context.SehirdekiYer, "Id", "SehirdekiYerAd");
            return View();
        }

        // POST: IlSehirYeri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IlId,SehirdekiYerId")] IlSehirYeri ilSehirYeri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ilSehirYeri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IlId"] = new SelectList(_context.Il, "Id", "IlAd", ilSehirYeri.IlId);
            ViewData["SehirdekiYerId"] = new SelectList(_context.SehirdekiYer, "Id", "SehirdekiYerAd", ilSehirYeri.SehirdekiYerId);
            return View(ilSehirYeri);
        }

        // GET: IlSehirYeri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilSehirYeri = await _context.IlSehirYeri.FindAsync(id);
            if (ilSehirYeri == null)
            {
                return NotFound();
            }
            ViewData["IlId"] = new SelectList(_context.Il, "Id", "IlAd", ilSehirYeri.IlId);
            ViewData["SehirdekiYerId"] = new SelectList(_context.SehirdekiYer, "Id", "SehirdekiYerAd", ilSehirYeri.SehirdekiYerId);
            return View(ilSehirYeri);
        }

        // POST: IlSehirYeri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IlId,SehirdekiYerId")] IlSehirYeri ilSehirYeri)
        {
            if (id != ilSehirYeri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ilSehirYeri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IlSehirYeriExists(ilSehirYeri.Id))
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
            ViewData["IlId"] = new SelectList(_context.Il, "Id", "IlAd", ilSehirYeri.IlId);
            ViewData["SehirdekiYerId"] = new SelectList(_context.SehirdekiYer, "Id", "SehirdekiYerAd", ilSehirYeri.SehirdekiYerId);
            return View(ilSehirYeri);
        }

        // GET: IlSehirYeri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilSehirYeri = await _context.IlSehirYeri
                .Include(i => i.Il)
                .Include(i => i.SehirdekiYer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilSehirYeri == null)
            {
                return NotFound();
            }

            return View(ilSehirYeri);
        }

        // POST: IlSehirYeri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ilSehirYeri = await _context.IlSehirYeri.FindAsync(id);
            _context.IlSehirYeri.Remove(ilSehirYeri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IlSehirYeriExists(int id)
        {
            return _context.IlSehirYeri.Any(e => e.Id == id);
        }
    }
}

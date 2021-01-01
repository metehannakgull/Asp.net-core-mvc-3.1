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
    public class IlKampController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IlKampController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IlKamp
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IlKamp.Include(i => i.Il).Include(i => i.KampYeri);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: IlKamp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilKamp = await _context.IlKamp
                .Include(i => i.Il)
                .Include(i => i.KampYeri)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilKamp == null)
            {
                return NotFound();
            }

            return View(ilKamp);
        }

        // GET: IlKamp/Create
        public IActionResult Create()
        {
            ViewData["IlId"] = new SelectList(_context.Il, "Id", "IlAd");
            ViewData["KampYeriId"] = new SelectList(_context.KampYeri, "Id", "KampYeriAd");
            return View();
        }

        // POST: IlKamp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IlId,KampYeriId")] IlKamp ilKamp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ilKamp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IlId"] = new SelectList(_context.Il, "Id", "IlAd", ilKamp.IlId);
            ViewData["KampYeriId"] = new SelectList(_context.KampYeri, "Id", "KampYeriAd", ilKamp.KampYeriId);
            return View(ilKamp);
        }

        // GET: IlKamp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilKamp = await _context.IlKamp.FindAsync(id);
            if (ilKamp == null)
            {
                return NotFound();
            }
            ViewData["IlId"] = new SelectList(_context.Il, "Id", "IlAd", ilKamp.IlId);
            ViewData["KampYeriId"] = new SelectList(_context.KampYeri, "Id", "KampYeriAd", ilKamp.KampYeriId);
            return View(ilKamp);
        }

        // POST: IlKamp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IlId,KampYeriId")] IlKamp ilKamp)
        {
            if (id != ilKamp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ilKamp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IlKampExists(ilKamp.Id))
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
            ViewData["IlId"] = new SelectList(_context.Il, "Id", "IlAd", ilKamp.IlId);
            ViewData["KampYeriId"] = new SelectList(_context.KampYeri, "Id", "KampYeriAd", ilKamp.KampYeriId);
            return View(ilKamp);
        }

        // GET: IlKamp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilKamp = await _context.IlKamp
                .Include(i => i.Il)
                .Include(i => i.KampYeri)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilKamp == null)
            {
                return NotFound();
            }

            return View(ilKamp);
        }

        // POST: IlKamp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ilKamp = await _context.IlKamp.FindAsync(id);
            _context.IlKamp.Remove(ilKamp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IlKampExists(int id)
        {
            return _context.IlKamp.Any(e => e.Id == id);
        }
    }
}

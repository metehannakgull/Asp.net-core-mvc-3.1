using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blogum2.Data;
using Blogum2.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Blogum2.Controllers
{
    public class KampYeriController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment; //hosting ortamı

        public KampYeriController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: KampYeri
        public async Task<IActionResult> Index()
        {
            return View(await _context.KampYeri.ToListAsync());
        }

        // GET: KampYeri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kampYeri = await _context.KampYeri
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kampYeri == null)
            {
                return NotFound();
            }

            return View(kampYeri);
        }

        // GET: KampYeri/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KampYeri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KampYeriAd,Resim")] KampYeri kampYeri)
        {
            /* if (ModelState.IsValid)
             {
                 _context.Add(kampYeri);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             return View(kampYeri);
             */
            if (ModelState.IsValid) //girdiğimiz veriler uygunsa
            {

                //******
                string webRootPath = _hostingEnvironment.WebRootPath; //projenin ana yolu
                var files = HttpContext.Request.Form.Files;


                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\resimler");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                kampYeri.Resim = @"\images\resimler\" + fileName + extension;

                //********

                _context.Add(kampYeri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            return View(kampYeri);
        }

        // GET: KampYeri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kampYeri = await _context.KampYeri.FindAsync(id);
            if (kampYeri == null)
            {
                return NotFound();
            }
            return View(kampYeri);
        }

        // POST: KampYeri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KampYeriAd,Resim")] KampYeri kampYeri)
        {
            if (id != kampYeri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kampYeri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KampYeriExists(kampYeri.Id))
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
            return View(kampYeri);
        }

        // GET: KampYeri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kampYeri = await _context.KampYeri
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kampYeri == null)
            {
                return NotFound();
            }

            return View(kampYeri);
        }

        // POST: KampYeri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kampYeri = await _context.KampYeri.FindAsync(id);
            _context.KampYeri.Remove(kampYeri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KampYeriExists(int id)
        {
            return _context.KampYeri.Any(e => e.Id == id);
        }
    }
}

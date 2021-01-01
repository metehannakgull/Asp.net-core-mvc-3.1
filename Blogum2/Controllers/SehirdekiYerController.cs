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
    public class SehirdekiYerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment; //hosting ortamı

        public SehirdekiYerController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: SehirdekiYer
        public async Task<IActionResult> Index()
        {
            return View(await _context.SehirdekiYer.ToListAsync());
        }

        // GET: SehirdekiYer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sehirdekiYer = await _context.SehirdekiYer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sehirdekiYer == null)
            {
                return NotFound();
            }

            return View(sehirdekiYer);
        }

        // GET: SehirdekiYer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SehirdekiYer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SehirdekiYerAd,Resim")] SehirdekiYer sehirdekiYer)
        {
            /*  if (ModelState.IsValid)
              {
                  _context.Add(sehirdekiYer);
                  await _context.SaveChangesAsync();
                  return RedirectToAction(nameof(Index));
              }
              return View(sehirdekiYer);
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
                sehirdekiYer.Resim = @"\images\resimler\" + fileName + extension;

                //********

                _context.Add(sehirdekiYer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sehirdekiYer);
        }

        // GET: SehirdekiYer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sehirdekiYer = await _context.SehirdekiYer.FindAsync(id);
            if (sehirdekiYer == null)
            {
                return NotFound();
            }
            return View(sehirdekiYer);
        }

        // POST: SehirdekiYer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SehirdekiYerAd,Resim")] SehirdekiYer sehirdekiYer)
        {
            if (id != sehirdekiYer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sehirdekiYer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SehirdekiYerExists(sehirdekiYer.Id))
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
            return View(sehirdekiYer);
        }

        // GET: SehirdekiYer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sehirdekiYer = await _context.SehirdekiYer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sehirdekiYer == null)
            {
                return NotFound();
            }

            return View(sehirdekiYer);
        }

        // POST: SehirdekiYer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sehirdekiYer = await _context.SehirdekiYer.FindAsync(id);
            _context.SehirdekiYer.Remove(sehirdekiYer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SehirdekiYerExists(int id)
        {
            return _context.SehirdekiYer.Any(e => e.Id == id);
        }
    }
}

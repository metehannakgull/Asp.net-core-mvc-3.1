using Blogum2.Data;
using Blogum2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Blogum2.Controllers
{
    
    public class HomeController : Controller

    {

       
        private readonly ApplicationDbContext _context;


        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            
        }

          public IActionResult Index()
          {
            /* var geziList = (from v in _context.IlKamp
                             join k in _context.KampYeri
                             on v.Id equals k.Id

                             select new GeziDTO
                             {
                                 KampYerID = k.Id,
                                 KampYerAdi = k.KampYeriAd,
                                 Resim = k.Resim,
                                 VilayetAd = v.Il.IlAd,
                                 IlceAd = v.Il.Ilce.IlceAd,

                             }).ToList();*/

            // var geziListKamp = _context.KampYeri;
            var db = _context.IlKamp
                .Include(f => f.KampYeri)
                .Include(f => f.Il);


            return View(db.ToList()) ;
          }
        /*    public async Task<IActionResult> Index()
      {
          var applicationDbContext = _context.IlKamp.Include(i => i.KampYeri.KampYeriAd)
              .Include(i =>i.KampYeri.Resim )
              .Include(i => i.Il.IlAd)
              .Include(i => i.Il.Ilce.IlceAd);
          return View(await applicationDbContext.ToListAsync());
      }*/

        [HttpPost]
        public IActionResult Language(string language, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(language)),
                 new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
            return LocalRedirect(returnUrl);
        }
        public IActionResult AboutMe()//hakkımda
        {
           
            ViewBag.writting2 = "Ben Metehan Akgül. Bilgisayar Mühendisiyim.";
            ViewBag.writting3 = "Hayatımın üçte birini uyuyarak geçirdiğim, geri kalan üçte ikisinin büyük bölümünün para karşılığı satın alınıp, işte çalışmam gerektiği, çevremdeki insanların kendilerine göre doğrularının dayatıldığı bir yerdeyim.";
            ViewBag.writting4 = "İnsanın temel ihtiyaçlarını karşılama problemi yüzyıllardır devam etmektedir. Günümüzde bunca bolluk içerisinde insanlara insan olarak değil, köle olarak bakılmaya devam ettikçe de bu problem çözülemez." +
                "Şu zamanın şartlarına göre ev almak için yıllarca çalışmak veya aynı ürüne daha fazla para verip satın almak için sürekli çalışmaya devam etmek gerek. " +
                "En değerli varlığımız olan zamanımızı sürekli kaybediyoruz. Bu stresli ve anlamsız zamanda yaşadığımı anladığım anlar gezip gördüğüm zamanlardır. Gerçekten insanı ne kadar çok geliştirdiğinin farkındayım. " +
                "Daha iyi bir gelecekte bu yazımı okuduğumda neler hissedeceğimi merak ediyorum. İnsanlığın 4 temel ihtiyacı olan yaşama, açlık, barınma ve sağlık sorunlarının daha az olduğu bir dünya ümidiyle. ";
            return View();
        }
        public IActionResult Contact()//iletişim
        {
            return View();
        }

        [Authorize]
        public IActionResult Admin()
        {
            return View();
        }
        
        /*
                public class GeziDTO
                {
                    public string KampYerAdi { get; internal set; }

                    public string Resim { get; internal set; }

                    public string VilayetAd { get; internal set; }

                    public string IlceAd { get; internal set; }

                    public int KampYerID { get; set; }



                }*/
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

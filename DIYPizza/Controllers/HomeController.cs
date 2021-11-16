using DIYPizza.Models;
using DIYPizza.Models.Data;
using DIYPizza.Models.Entity;
using DIYPizza.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DIYPizza.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _db; 

        public HomeController(DatabaseContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Secim> secimler = _db.Malzemeler.Select(x => new Secim()
            {
                MalzemeId = x.Id,
                MalzemeAd = x.Ad,
                SeciliMi = false
            }
            ).ToList();

            var vm = new SiparisViewModel()
            {
                Secimler = secimler
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(SiparisViewModel vm)
        {
            if (vm.Secimler.Count(x => x.SeciliMi) < 2)
            {
                ModelState.AddModelError("", "En az 2 adet malzeme seçmeniz gerekmektedir.");
            }

            if (ModelState.IsValid)
            {
                var secilenler = vm.Secimler.Where(x => x.SeciliMi).ToList();
                //TempData["SecilenMalzemeler"] = JsonSerializer.Serialize(secilenler);
                int[] secimIdler = secilenler.Select(s => s.MalzemeId).ToArray();

                Siparis siparis = new Siparis()
                {
                    Adres = vm.Adres,
                    Malzemeler = _db.Malzemeler.Where(m => secimIdler.Contains(m.Id)).ToList()
                };
                _db.Add(siparis);
                _db.SaveChanges();

                return RedirectToAction("Siparis", new { siparisNo = siparis.Id });
            }

            return View(vm);
        }

        public IActionResult Siparis()
        {
            string json = (string)TempData["SecilenMalzemeler"];
            List<Secim> secimler = JsonSerializer.Deserialize<List<Secim>>(json);
            return View(secimler);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

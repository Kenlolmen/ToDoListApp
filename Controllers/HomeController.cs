using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using System.Diagnostics;
using ToDoListApp.Models;


namespace ToDoListApp.Controllers
{
    public class HomeController : Controller
    {
        private ZadanieContext context;
        public HomeController(ZadanieContext ctx) => context = ctx;

        public IActionResult Index(string id)
        {
            var filtry = new Filtry(id);
            ViewBag.Filtry = filtry;

            ViewBag.Kategorie = context.Kategorie.ToList();
            ViewBag.Statusy = context.Statusy.ToList();
            ViewBag.DueFilters = ToDoListApp.Models.Filtry.DueFilterValues;


            IQueryable<Zadanie> kolejka = context.Zadania
                .Include(n => n.Kategoria)
                .Include(n => n.Status);

            if (filtry.CzyKategoria)
            {
                kolejka = kolejka.Where(n => n.KategoriaIDKategoria == filtry.IDKategoria);
            }

            if (filtry.CzyStatus)
            {
                kolejka = kolejka.Where(n => n.StatusIDStatus == filtry.IDStatus);
            }

            if (filtry.CzyDue)
            {
                var dzis = DateTime.Today;
                if (filtry.CzyPrzeszlosc)
                {
                    kolejka = kolejka.Where(n => n.Date < dzis);
                }
                else if (filtry.CzyPrzyszlosc)
                {
                    kolejka = kolejka.Where(n => n.Date > dzis);
                }
                else if (filtry.CzyDzis)
                {
                    kolejka = kolejka.Where(n => n.Date == dzis);
                }
            }
            var zadania = kolejka.OrderBy(n => n.Date).ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Dodaj()
        {
            ViewBag.Kategorie = context.Kategorie.ToList();
            ViewBag.Statusy = context.Statusy.ToList();
            var zadanie = new Zadanie { StatusIDStatus = "otwarte" };
            return View(zadanie);
        }
        [HttpPost]
        public IActionResult Dodaj(Zadanie zadanie)
        {
            if (ModelState.IsValid)
            {
;

                // SprawdŸ, czy IDStatus jest poprawnie przypisany
                if (string.IsNullOrEmpty(zadanie.StatusIDStatus))
                {
                    ModelState.AddModelError("StatusIDStatus", "Status jest wymagany."); // Dodaj b³¹d do modelu
                }

                if (ModelState.IsValid)
                {
                    context.Zadania.Add(zadanie);
                    context.SaveChanges(); // Tutaj mo¿e wyst¹piæ b³¹d
                    return RedirectToAction("Index");
                }
            }
            /*
            if (ModelState.IsValid)
            {
                context.Zadania.Add(zadanie);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Kategorie = context.Kategorie.ToList();
                ViewBag.Statusy = context.Statusy.ToList();
                return View(zadanie);
            }
            */
            if (string.IsNullOrEmpty(zadanie.KategoriaIDKategoria))
            {
                ModelState.AddModelError("IDKategoria", "Kategoria jest wymagana.");
                ViewBag.Kategorie = context.Kategorie.ToList();
                ViewBag.Statusy = context.Statusy.ToList();
                return View(zadanie);
            }
            else
            {

                try
                {
                    context.Zadania.Add(zadanie);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    // Logowanie b³êdu
                    var innerException = ex.InnerException?.Message;
                    ModelState.AddModelError("", $"Wyst¹pi³ b³¹d: {innerException}");
                    ViewBag.Kategorie = context.Kategorie.ToList();
                    ViewBag.Statusy = context.Statusy.ToList();
                    return View(zadanie);
                }
            }
            
        }
        [HttpPost]
        public IActionResult Filter(string category, string due, string status)//string[] filtry )
        {
            //  string id = string.Join("-", filtry);
            string filterString = $"{category}-{due}-{status}";
            return RedirectToAction("index", new { ID = filterString });
        }
        [HttpPost]
        public IActionResult Ozn_Zrobione([FromRoute] string id, Zadanie wybrane)
        {
            wybrane = context.Zadania.Find(wybrane.Id)!;

            if (wybrane != null)
            {
                wybrane.StatusIDStatus = "zamkniete";
                context.SaveChanges();
            }
            return RedirectToAction("index", new { ID = id });
        }

        [HttpPost]
        public IActionResult UsunZrobione(string id)
        {
            var usun = context.Zadania.Where(n => n.StatusIDStatus == "zamkniete").ToList();

            foreach (var zadanie in usun)
            {
                context.Zadania.Remove(zadanie);
            }
            context.SaveChanges();

            return RedirectToAction("index", new { id = id });
        }

    }
}

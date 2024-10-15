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

             var zadania = context.Zadania.Include(z => z.Kategoria).Include(z => z.Status).ToList();

            return View(zadania);
        }

        [HttpGet]
        public IActionResult Dodaj()
        {
            ViewBag.Kategorie = context.Kategorie.ToList();
            ViewBag.Statusy = context.Statusy.ToList();
            var zadanie = new Zadanie { StatusIDStatus = "otw" };
            return View(zadanie);
        }
        [HttpPost]
        public IActionResult Dodaj(Zadanie zadanie)
        {
            if (ModelState.IsValid)
            {
            
                if (string.IsNullOrEmpty(zadanie.StatusIDStatus))
                {
                    ModelState.AddModelError("StatusIDStatus", "Status jest wymagany."); 
                }

                if (ModelState.IsValid)
                {
                    context.Zadania.Add(zadanie);
                    context.SaveChanges(); 
                    return RedirectToAction("Index");
                }
            }
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
            
            category = string.IsNullOrEmpty(category) ? "all" : category;
            due = string.IsNullOrEmpty(due) ? "all" : due;
            status = string.IsNullOrEmpty(status) ? "all" : status;

            string filterString = $"{category}-{due}-{status}";
            return RedirectToAction("index", new { ID = filterString });
        }
        [HttpPost]
        public IActionResult Ozn_Zrobione([FromRoute] string id, Zadanie wybrane)
        {
            wybrane = context.Zadania.Find(wybrane.Id)!;

            if (wybrane != null)
            {
                wybrane.StatusIDStatus = "zam";
                context.SaveChanges();
            }
            return RedirectToAction("index", new { ID = id });
        }

        [HttpPost]
        public IActionResult UsunZrobione(string id)
        {
            var usun = context.Zadania.Where(n => n.StatusIDStatus == "zam").ToList();

            foreach (var zadanie in usun)
            {
                context.Zadania.Remove(zadanie);
            }
            context.SaveChanges();

            return RedirectToAction("index", new { ID = id });
        }

    }
}

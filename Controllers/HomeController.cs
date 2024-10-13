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
                kolejka = kolejka.Where(n => n.IDKategoria == filtry.IDKategoria);
            }

            if (filtry.CzyStatus)
            {
                kolejka = kolejka.Where(n => n.IDStatus == filtry.IDStatus);
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
            var zadanie = new Zadanie { IDStatus = "otwarte" };
            return View(zadanie);
        }
        [HttpPost]
        public IActionResult Dodaj(Zadanie zadanie)
        {
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
                wybrane.IDStatus = "zamkniete";
                context.SaveChanges();
            }
            return RedirectToAction("index", new { ID = id });
        }

        [HttpPost]
        public IActionResult UsunZrobione(string id)
        {
            var usun = context.Zadania.Where(n => n.IDStatus == "zamkniete").ToList();

            foreach (var zadanie in usun)
            {
                context.Zadania.Remove(zadanie);
            }
            context.SaveChanges();

            return RedirectToAction("index", new { id = id });
        }

    }
}

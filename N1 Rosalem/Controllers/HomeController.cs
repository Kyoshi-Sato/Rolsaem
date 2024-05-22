using BarApp.Controllers;
using BarApp.Data;
using BarApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using N1_Rosalem.Models;
using System.Diagnostics;

namespace N1_Rosalem.Controllers
{
    public class HomeController : Controller
    {
        private readonly BarContext _context;



        public HomeController(BarContext context)
        {
            _context = context;

        }

        // GET: Bebidas
        public IActionResult Index()
        {
            return View(_context.Bebida.ToList());
        }

        public IActionResult SortDrinks(string sortBy)
        {
            var bebidas = _context.Bebida.AsQueryable();

            switch (sortBy)
            {
                case "Nome":
                    bebidas = bebidas.OrderBy(b => b.Nome);
                    break;
                case "Preco":
                    bebidas = bebidas.OrderBy(b => b.Preco);
                    break;
                default:
                    bebidas = bebidas.OrderBy(b => b.Nome);
                    break;
            }

            return PartialView("_DrinkListPartial", bebidas.ToList());
        }

        // GET: Bebidas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bebida = await _context.Bebida.FirstOrDefaultAsync(m => m.Id == id);
            if (bebida == null)
            {
                return NotFound();
            }

            return View(bebida);
        }

        // GET: Bebidas/Create


        public IActionResult Privacy()
        {
            return View();
        }

    }
}


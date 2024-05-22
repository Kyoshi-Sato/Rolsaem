using Microsoft.AspNetCore.Mvc;
using BarApp.Models;
using System.Linq;
using BarApp.Data;
using N1_Rosalem.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using N1_Rosalem.Models;

namespace BarApp.Controllers
{
    public class BebidasController : Controller
    {
        private readonly BarContext _context;

        public BebidasController(BarContext context)
        {
            _context = context;

        }

        // GET: Bebidas
        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.CurrentFilter = searchString;

            var bebida = from b in _context.Bebida
                          select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                bebida = bebida.Where(b => b.Nome.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    bebida = bebida.OrderByDescending(b => b.Nome);
                    break;
                case "Price":
                    bebida = bebida.OrderBy(b => b.Preco);
                    break;
                case "price_desc":
                    bebida = bebida.OrderByDescending(b => b.Preco);
                    break;
                default:
                    bebida = bebida.OrderBy(b => b.Nome);
                    break;
            }

            ViewData["BebidasFiltradas"] = bebida.ToList(); // Definir dados filtrados no ViewData
            
            return View();
        }

        [AdminOnly]

        // GET: Bebidas/Create
        public IActionResult Criar()
        {
           
            var origens = _context.Origem.ToList();
            var receitas = _context.Receita.ToList();

            ViewBag.Origens = new SelectList(_context.Origem.ToList(), "id", "origem");
            ViewBag.Receitas = new SelectList(_context.Receita.ToList(), "id", "receita");


            return View();
        }

        // POST: Bebidas/Create
        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar([Bind("Nome,Preco,Estoque,Descricao,ImagemURL,OrigemId,ReceitaId")] Bebida bebida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bebida);
                _context.SaveChanges();
                var bebidas = _context.Bebida.ToList();
                return PartialView("_BebidasPartial", bebidas);
            }

            return BadRequest(ModelState);
        }

        // GET: Bebidas/Edit/5
        [AdminOnly]

        public IActionResult Edit(int id)
        {
            var bebida = _context.Bebida.Find(id);
            if (bebida == null)
            {
                return NotFound();
            }
            return View(bebida);
        }

        // POST: Bebidas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]

        public IActionResult Edit(int id, [Bind("Id,Nome,Preco,Estoque,Descricao, ImagemURL")] Bebida bebida)
        {
            if (id != bebida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(bebida);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(bebida);
        }

        // GET: Bebidas/Details/5
        public IActionResult Details(int id)
        {
            var bebida = _context.Bebida.Find(id);
            if (bebida == null)
            {
                return NotFound();
            }
            return View(bebida);
        }

        // POST: Bebidas/Delete/5
        [AdminOnly]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var bebida = _context.Bebida.Find(id);
            if (bebida != null)
            {
                _context.Bebida.Remove(bebida);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult BebidasPartial(string sortOrder, string searchString)
        {
            // Lógica para filtrar e classificar as bebidas
            var bebidasFiltradas = from b in _context.Bebida
                                   select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                bebidasFiltradas = bebidasFiltradas.Where(b => b.Nome.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    bebidasFiltradas = bebidasFiltradas.OrderByDescending(b => b.Nome);
                    break;
                case "Price":
                    bebidasFiltradas = bebidasFiltradas.OrderBy(b => b.Preco);
                    break;
                case "price_desc":
                    bebidasFiltradas = bebidasFiltradas.OrderByDescending(b => b.Preco);
                    break;
                default:
                    bebidasFiltradas = bebidasFiltradas.OrderBy(b => b.Nome);
                    break;
            }

            return PartialView("_BebidasPartial", bebidasFiltradas.ToList());
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using BarApp.Models;
using System.Linq;
using BarApp.Data;
using N1_Rosalem.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using N1_Rosalem.Models;
using N1_Rosalem.Services;
using Microsoft.EntityFrameworkCore;


namespace BarApp.Controllers
{
    public class BebidasController : Controller
    {
        private readonly BarContext _context;
        private readonly MappingService _mappingService;

        public BebidasController(BarContext context, MappingService mappingService)
        {
            _context = context;
            _mappingService = mappingService;


        }

        // GET: Bebidas
        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.CurrentFilter = searchString;

            var bebidas = from b in _context.Bebida
                          select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                bebidas = bebidas.Where(b => b.Nome.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    bebidas = bebidas.OrderByDescending(b => b.Nome);
                    break;
                case "Price":
                    bebidas = bebidas.OrderBy(b => b.Preco);
                    break;
                case "price_desc":
                    bebidas = bebidas.OrderByDescending(b => b.Preco);
                    break;
                default:
                    bebidas = bebidas.OrderBy(b => b.Nome);
                    break;
            }

            var bebidaViewModels = bebidas.Select(b => _mappingService.MapToBebidaViewModel(b)).ToList();
            return View(bebidaViewModels);
        }


        // GET: Bebidas/Create
        [AdminOnly]
        public IActionResult Criar()
        {
            ViewBag.Origens = new SelectList(_context.Origem.ToList(), "Id", "origem");
            ViewBag.Receitas = new SelectList(_context.Receita.ToList(), "Id", "receita");
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
                var bebidaViewModels = bebidas.Select(b => _mappingService.MapToBebidaViewModel(b)).ToList();
                return PartialView("_BebidasPartial", bebidaViewModels);
            }

            ViewBag.Origens = new SelectList(_context.Origem.ToList(), "Id", "origem", bebida.OrigemId);
            ViewBag.Receitas = new SelectList(_context.Receita.ToList(), "Id", "receita", bebida.ReceitaId);
            return View(bebida);
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

            ViewBag.Origens = new SelectList(_context.Origem.ToList(), "Id", "origem", bebida.OrigemId);
            ViewBag.Receitas = new SelectList(_context.Receita.ToList(), "Id", "receita", bebida.ReceitaId);
            return View(bebida);
        }


        // POST: Bebidas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public IActionResult Edit(int id, [Bind("Id,Nome,Preco,Estoque,Descricao,ImagemURL,OrigemId,ReceitaId")] Bebida bebida)
        {
            if (id != bebida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bebida);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Bebida.Any(e => e.Id == bebida.Id))
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

            ViewBag.Origens = new SelectList(_context.Origem.ToList(), "Id", "origem", bebida.OrigemId);
            ViewBag.Receitas = new SelectList(_context.Receita.ToList(), "Id", "receita", bebida.ReceitaId);
            return View(bebida);
        }


        // GET: Bebidas/Details/5
        public IActionResult Details(int id)
        {
            var bebida = _context.Bebida
                .Include(b => b.Origem)
                .Include(b => b.Receita)
                .FirstOrDefault(m => m.Id == id);

            if (bebida == null)
            {
                return NotFound();
            }

            var bebidaViewModel = _mappingService.MapToBebidaViewModel(bebida);
            return View(bebidaViewModel);
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

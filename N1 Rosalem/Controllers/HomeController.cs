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
            //_logger = logger;
        }

        // GET: Bebidas
        public IActionResult Index()
        {
            return View(_context.Bebidas.ToList());
        }

        // GET: Bebidas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bebida = await _context.Bebidas.FirstOrDefaultAsync(m => m.Id == id);
            if (bebida == null)
            {
                return NotFound();
            }

            return View(bebida);
        }

        // GET: Bebidas/Create
        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar([Bind("Nome,Preco,Estoque")] Bebida bebida)
        {

            if (ModelState.IsValid)
            {

                _context.Add(bebida);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(bebida);
        }


        // GET: Bebidas/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bebida = _context.Bebidas.Find(id);
            if (bebida == null)
            {
                return NotFound();
            }
            return View(bebida);
        }

        // POST: Bebidas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,Descricao,Estoque")] Bebida bebida)
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
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BebidaExists(bebida.Id))
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
            return View(bebida);
        }

        private bool BebidaExists(int id)
        {
            return _context.Bebidas.Any(e => e.Id == id);
        }

        // GET: Bebidas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bebida = await _context.Bebidas.FirstOrDefaultAsync(m => m.Id == id);
            if (bebida == null)
            {
                return NotFound();
            }

            return View(bebida);
        }

        // POST: Bebidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bebida = await _context.Bebidas.FindAsync(id);
            _context.Bebidas.Remove(bebida);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BebidaExiste(int id)
        {
            return _context.Bebidas.Any(e => e.Id == id);
        }
    }
}


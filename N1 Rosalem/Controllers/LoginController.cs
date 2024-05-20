using Microsoft.AspNetCore.Mvc;
using N1_Rosalem.Models;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using BarApp.Data;

namespace N1_Rosalem.Controllers
{
    public class LoginController : Controller
    {
        private readonly BarContext _context;

        public LoginController(BarContext context)
        {
            _context = context;
        }

        // GET: Login/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Usuarios.SingleOrDefault(u => u.Nome == model.Nome && u.Senha == model.Senha);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Nome)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Nome de usuário ou senha inválidos.");
            }
            return View(model);
        }

        // GET: Login/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Login/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("Nome,EstadoCivil,Idade,Senha")] UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction(nameof(Login));
            }
            return View(usuario);
        }

        // POST: Login/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}

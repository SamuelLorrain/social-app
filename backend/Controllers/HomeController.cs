using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using backend.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace backend.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("login")]
    public IActionResult Login(string returnUrl)
    {
        ViewData["ReturnUrl"] = System.Web.HttpUtility.UrlEncode(returnUrl);
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> ValidateAsync(string username, string password, string returnUrl)
    {
        if (username == "foo" && password == "bar") {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("username", username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);

            return Redirect(returnUrl);
        }
        return BadRequest();
    }

    public IActionResult Register()
    {
        return View();
    }

    [Authorize]
    public IActionResult OnlyLogged()
    {
        return View();
    }

    [Authorize]
    public IActionResult Logout()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace backend.Controllers;

public class HomeController : Controller
{
    public HomeController() {}

    [HttpGet("/login"), HttpGet("/")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("/login")]
    public async Task<IActionResult> ValidateAsync(string username, string password)
    {
        if (username == "foo" && password == "bar")
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("username", username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
            claims.Add(new Claim(ClaimTypes.Name, username));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);

            return Redirect(Url.Action("Spa"));
        }
        TempData["Error"] = "login incorrect";
        return View("login");
    }

    [Authorize]
    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/");
    }

    [Authorize]
    public IActionResult Spa()
    {
        return View();
    }
}

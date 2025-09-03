using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRegistration.Models;
using StudentRegistration.Models.Login;
using System.Security.Claims;

public class AuthController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Fetch user by username
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

        // Validate user existence and password
        if (user == null || !VerifyPassword(model.Password, user.Password))
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(model);
        }

        // Create user claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //new Claim(ClaimTypes.Role, user.Role ?? "User")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            ///IsPersistent = model.isrmemberme // optional: handle remember me
        };

        // Sign in the user
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );

        return RedirectToAction("Index", "Home");
    }

    // Verifies a plaintext password against a hashed one using BCrypt
    private bool VerifyPassword(string inputPassword, string storedHash)
    {
        //string plainPassword = "admin@123";
        //string hashedPassword = "$2b$10$UtcFVvwz7OdC8TglNRYMsOoDuX.HLRb4T.Xt4RIwqL5de6aZEBXFG";

        //bool isValid = BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        //Console.WriteLine(isValid ? "Password matches!" : "Password does NOT match.");

        if (string.IsNullOrWhiteSpace(storedHash))
            return false;

        try
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
        }
        catch (SaltParseException)
        {
            // Invalid hash format
            return false;
        }
    }

    [HttpPost]
    [Route("auth/logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}

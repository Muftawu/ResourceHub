using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using group4.Models;
using Microsoft.AspNetCore.Identity;

namespace group4.Controllers;

public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> SignUp(SignUpViewModel user)
    {
        if (string.IsNullOrEmpty(user.Password))
        {
            ModelState.AddModelError("Password", "Password is required.");
            return View(user);
        }
        var newUser = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            UserName = user.Email,
        };

        var result = await _userManager.CreateAsync(newUser, user.Password);
        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = "Account successfully Created. Please login to continue";
            return RedirectToAction("Login");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"{error.Description}");
            }
        }
        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        int userId = 3;
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user != null)
        {
            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            ViewBag.Email = user.Email;
            ViewBag.Phone = user.Phone;
        }
        else
        {
            TempData["ErrorMessages"] = $"User with {userId} not found";
        }
        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> Users()
    {
        var users = await _userManager.Users.ToListAsync();
        return View(users);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel user)
    {   
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, isPersistent:false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = $"Welcome back, {user.Username}";
                return RedirectToAction("MyUploads", "Resource");
            }
            else
            {
                TempData["ErrorMessage"] = "Username or Password is incorrect";
            }

        }
        return View(user);
    }


    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

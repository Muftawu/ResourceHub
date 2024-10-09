using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using group4.Data;
 using group4.Models;

namespace group4.Controllers;

public class UserController : Controller
{
    private readonly ApplicationDbContext _context;
    
    // Dependency Injection
    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> GetUser()
    {
        int userid = 3;
        var user = await _context.Users.FindAsync(userid);
        if (user != null) {
            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            ViewBag.Email = user.Email;
            ViewBag.Phone = user.Phone;
        }
        else {
            Console.WriteLine($"user with id {userid} not found");
        }
        return View(user);
    }

    public async Task<IActionResult> Users()
    {
        List<User> users = await _context.Users.ToListAsync();
        return View(users);
    }

    public async Task<IActionResult> CreateUser()
    {
        var newuser = new User
        {
            FirstName = "Muftawu",
            LastName = "Yiwere",
            Email = "mohammedyiwere@gmail.com",
            Phone = "+233545723325"
        };

        _context.Users.Add(newuser);
        await _context.SaveChangesAsync();
        Console.WriteLine($"user created successfully");
        return RedirectToAction("Users"); // that was very quick in the application! chale dotnet dey bee waa
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

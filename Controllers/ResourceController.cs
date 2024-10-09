using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using group4.Data;
 using group4.Models;

namespace group4.Controllers;

public class ResourceController : Controller
{
    private readonly ApplicationDbContext _context;
    
    // Dependency Injection
    public ResourceController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IActionResult MyUploads()
    {
        return View();
    }

    public IActionResult Upload()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

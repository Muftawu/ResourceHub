using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using group4.Data;
using group4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace group4.Controllers;

public class ResourceController : Controller
{
    private readonly ApplicationDbContext _context;

    public ResourceController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> MyUploads()
    {
        List<Resource> resources = await _context.Resources.ToListAsync();
        return View(resources);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Upload()
    {   
        List<Topic> topics = await _context.Topics.ToListAsync();
        List<Course> courses = await _context.Courses.ToListAsync();

        ViewBag.topics = topics;
        ViewBag.courses = courses;
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Upload(Resource resource, IFormFile file)
    {
        var userId = 1;
        resource.UserId = userId.ToString();

        if (ModelState.IsValid)
        {
            if (file != null && file.Length > 0) 
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var fileName = Path.GetFileName(file.FileName); 
                var fullPath = Path.Combine(uploadPath, fileName); 

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                resource.FilePath = fullPath;

                _context.Resources.Add(resource);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "File uploaded successfully!";
                return RedirectToAction("MyUploads");
            }
            else
            {
                TempData["ErrorMessage"] = "No file uploaded.";
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to upload resource.";
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }

        ViewBag.topics = await _context.Topics.ToListAsync();
        ViewBag.courses = await _context.Courses.ToListAsync();
        return View(resource);
    }

    [Authorize]
    public async Task<IActionResult> Search(String searchItem)
    {
        List<Resource> resources = await _context.Resources.ToListAsync();
        return View(resources);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

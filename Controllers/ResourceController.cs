using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using group4.Data;
using group4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace group4.Controllers
{
    [Authorize]
    public class ResourceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ResourceController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> MyUploads()
        {
            // Get the logged-in user's ID
            var userId = _userManager.GetUserId(User);

            // Fetch the resources uploaded by the user
            var resources = await _context.Resources
                .Where(r => r.UserId == userId)  // Use '==' to match exact UserId
                .ToListAsync();

            // Extract the file name for each resource
            foreach (var resource in resources)
            {
                resource.FilePath = Path.GetFileName(resource.FilePath);  // Update FilePath to only hold the file name
            }

            return View(resources);
        }



        [HttpGet]
        public async Task<IActionResult> Upload()
        {
            List<Topic> topics = await _context.Topics.ToListAsync();
            List<Course> courses = await _context.Courses.ToListAsync();

            ViewBag.topics = topics;
            ViewBag.courses = courses;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(Resource resource, IFormFile file)
        {
            var userId = _userManager.GetUserId(User);
            resource.UserId = userId;

            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    // var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");

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

        public async Task<IActionResult> Search(string searchItem)
        {
            if (string.IsNullOrEmpty(searchItem))
            {
                return View(new List<Resource>());
            }

            var resources = await _context.Resources.Where(r => r.Name.Contains(searchItem)).ToListAsync();
            

            // Extract the file name for each resource
            foreach (var resource in resources)
            {
                resource.FilePath = Path.GetFileName(resource.FilePath);  // Update FilePath to only hold the file name
            }
            ViewBag.searchItem = searchItem;
            return View(resources);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

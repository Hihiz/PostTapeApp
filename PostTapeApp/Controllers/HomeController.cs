using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostTapeApp.Data;
using PostTapeApp.Models;
using System.Diagnostics;

namespace PostTapeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _db;

        public HomeController(ApplicationContext db) => _db = db;

        public async Task<IActionResult> Index(string search)
        {
            IQueryable<Post> post = _db.Posts.Include(p => p.User);

            if (search != null)
            {
                if (!String.IsNullOrEmpty(search))
                {
                    post = post.Where(p => p.User.Name!.Contains(search));
                }
                return View(await post.ToListAsync());
            }

            return View(await post.ToListAsync());
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
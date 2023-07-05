using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post post = await _db.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_db.Users, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, UserId, DatePublish, Message")] Post post)
        {
            if (ModelState.IsValid)
            {
                _db.Posts.Add(post);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["UserId"] = new SelectList(_db.Users, "Id", "Name", post.UserId);

            return View(post);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Posts == null)
            {
                return NotFound();
            }

            Post post = await _db.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            ViewData["UserId"] = new SelectList(_db.Users, "Id", "Name", post.UserId);

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, UserId, DatePublish, Message")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Posts.Update(post);
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["UserId"] = new SelectList(_db.Users, "Id", "Name", post.UserId);

            return View(post);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post post = await _db.Posts.FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Posts == null)
            {
                return Problem("Entity set 'ApplicationContext.Posts'  is null.");
            }

            Post post = await _db.Posts.FindAsync(id);

            if (post != null)
            {
                _db.Posts.Remove(post);
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostTapeApp_WebApi.Data;
using PostTapeApp_WebApi.Models;

namespace PostTapeApp_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public PostController(ApplicationContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> Get()
        {
            return Ok(await _db.Posts.Include(p => p.User).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetId(int id)
        {
            IQueryable<Post> post = _db.Posts.Include(u => u.User).Where(u => u.Id == id);

            if (post == null)
            {
                return BadRequest("Запись не найдена");
            }

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            _db.Posts.Add(post);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> UpdatePost(int id, Post request)
        {
            if (id != request.Id)
            {
                return BadRequest($"Id введен не верно");
            }

            Post post = await _db.Posts.FindAsync(id);

            if (post == null)
            {
                return BadRequest($"Пост с Id: {id}, не найден");
            }

            post.UserId = request.UserId;
            post.DatePublish = request.DatePublish;
            post.Message = request.Message;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return NotFound();
            }

            return Ok(await _db.Posts.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeletePost(int id)
        {
            Post post = await _db.Posts.FindAsync(id);

            if (post == null)
            {
                return BadRequest("Пост не найден");
            }

            _db.Posts.Remove(post);
            _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
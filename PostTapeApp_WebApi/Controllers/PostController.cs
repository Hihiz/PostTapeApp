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
            IQueryable post = _db.Posts.Include(u => u.User).Where(u => u.Id == id);

            if (post == null)
            {
                return BadRequest("Запись не найдена");
            }

            return Ok(post);
        }
    }
}
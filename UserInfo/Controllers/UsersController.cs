using Microsoft.AspNetCore.Mvc;
using UserInfo.Models;
using UserInfo.Services;

namespace UserInfo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly InMemoryUserStore _store;

        public UsersController(InMemoryUserStore store)
        {
            _store = store;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return Ok(_store.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var u = _store.Get(id);
            if (u == null) return NotFound();
            return Ok(u);
        }

        [HttpPost]
        public ActionResult<User> Create([FromBody] User user)
        {
            var created = _store.Add(user);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            if (!_store.Update(id, user)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_store.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WebApplicationPR.Models;

namespace WebApplicationPR.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(UserData.Users);
        }
        
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = UserData.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound("Пользователь не найден.");
            return Ok(user);
        }
        
        [HttpPost]
        public ActionResult<User> AddUser([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Name))
                return BadRequest("Имя не может быть пустым.");

            user.Id = UserData.Users.Any() ? UserData.Users.Max(u => u.Id) + 1 : 1;
            UserData.Users.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = UserData.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound("Пользователь не найден.");
            
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Preferences = updatedUser.Preferences;
            return Ok(user);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = UserData.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound("Пользователь не найден.");

            UserData.Users.Remove(user);
            return NoContent();
        }
    }
}
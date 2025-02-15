using Microsoft.AspNetCore.Mvc;
using ExerciseDapper.Models;
using ExerciseDapper.Repositories;

namespace ExerciseDapper.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public UserController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddUser([FromBody] Users user)
    {
        if (string.IsNullOrWhiteSpace(user.Name) || user.Age <= 0)
        {
            return BadRequest("Invalid input data.");
        }

        bool isInserted = await _userRepository.InsertUserAsync(user);

        if (isInserted)
            return Ok(new { message = "User added successfully" });

        return StatusCode(500, "Error inserting user.");
    }
}

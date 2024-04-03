using DevFreela.API.Model;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var userViewModel = _userService.GetUser(id);
        if (userViewModel == null) return NotFound();
        return Ok(userViewModel);
    }

    [HttpPost]
    public IActionResult Post([FromBody] NewUserInputModel inputModel)
    {
        var id = _userService.Create(inputModel);
        return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
    }

    [HttpPut("{id}/login")]
    public IActionResult Login(int id, [FromBody] LoginModel loginModel)
    {
        return NoContent();
    }
}

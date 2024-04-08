using DevFreela.API.Model;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var getUserQuery = new GetUserQuery(id);
        var userViewModel = await _mediator.Send(getUserQuery);
        if (userViewModel == null) return NotFound();
        return Ok(userViewModel);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
    {
        if (!ModelState.IsValid)
        {
            var messages = ModelState
                .SelectMany(ms => ms.Value?.Errors!)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(messages);
        }
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = id }, command);
    }

    [HttpPut("/login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var loginUserViewModel = await _mediator.Send(command);
        if (loginUserViewModel == null) return BadRequest();
        return Ok(loginUserViewModel);
    }
}

using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/projects")]
public class ProjectsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(string query)
    {
        var getAllProjectsQuery = new GetAllProjectsQuery(query);
        var projects =await mediator.Send(getAllProjectsQuery);
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var projectByIdQuery = new GetProjectByIdQuery(id);
        var project = await mediator.Send(projectByIdQuery);
        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteProjectCommand(id);
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id}/comments")]
    public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/start")]
    public async Task<IActionResult> Start(int id)
    {
        var command = new StartProjectCommand(id);
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/finish")]
    public async Task<IActionResult> Finish(int id)
    {
        var command = new FinishProjectCommand(id);
        await mediator.Send(command);
        return NoContent();
    }
}

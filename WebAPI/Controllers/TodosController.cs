using Domain.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TodosController : ControllerBase
{
    private ITodoHome todoHome;

    public TodosController(ITodoHome todoHome)
    {
        this.todoHome = todoHome;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Todo>>> GetAll()
    {
        try
        {
            ICollection<Todo> todos = await todoHome.GetAsync();
            return Ok(todos);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> AddTodo([FromBody] Todo todo)
    {
        try
        {
            Todo added = await todoHome.AddAsync(todo);
            return Created($"todos/{added.Id}", added);
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Todo>> GetById([FromRoute]int id)
    {
        try
        {
            Todo todos = await todoHome.GetByIdAsync(id);
            return todos;
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> DeleteById([FromRoute] int id)
    {
        try
        {
            await todoHome.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> Update([FromBody] Todo todo)
    {
        try
        {
            await todoHome.UpdateAsync(todo);
            return Ok();
        }
        catch (Exception e)
        {
          return  StatusCode(500, e.Message);
        }
    }

}
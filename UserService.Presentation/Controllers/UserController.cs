using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Model;
using UserService.Application.Users.CreateUser;
using UserService.Application.Users.DeleteUser;
using UserService.Application.Users.GetAllUsers;
using UserService.Application.Users.GetUserById;
using UserService.Application.Users.UpdateUser;

namespace UserService.Presentation.Controllers;

public class UserController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllUsersQuery());
        return result.Match(
            users => Ok(users),
            errors => Problem(errors.First().Description));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetUserByIdQuery(id));
        return result.Match(
            user => Ok(user),
            errors => Problem(errors.First().Description));
    }
   
    [HttpPost]
    public async Task<IActionResult> CreateAdmin([FromBody] CreateUser model)
    {
        var result = await mediator.Send(new CreateUserCommand(model.Name, model.Email, model.Password));
        return result.Match(
            user => Ok(user),
            errors => Problem(errors.First().Description));
    }
    
    [HttpPut ("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateUser model)
    {
        var result = await mediator.Send(new UpdateUserCommand(model.Id,model.Name));
        return result.Match(
            success => Ok(success),
            errors => Problem(errors.First().Description));
    }
    
    [HttpDelete ("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await mediator.Send(new DeleteUserCommand(id));
        return result.Match(
            success => Ok(success),
            errors => Problem(errors.First().Description));
    }
}
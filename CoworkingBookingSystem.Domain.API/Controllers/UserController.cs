using CoworkingBookingSystem.Domain.Commands;
using CoworkingBookingSystem.Domain.Commands.UserCommands;
using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Handlers;
using CoworkingBookingSystem.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoworkingBookingSystem.Domain.API.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class UserController : ControllerBase
{
    [Route("GetByEmail")]
    [HttpGet]
    public UserEntity GetUserByEmail([FromServices] IUserRepository userRepository, [FromQuery] string email)
    {
        return userRepository.GetByEmail(email);
    }
    
    [Route("GetById/{userId}")]
    [HttpGet]
    public UserEntity GetUserById([FromServices] IUserRepository userRepository,Guid userId)
    {
        return userRepository.GetById(userId);
    }
    
    [Route("")]
    [HttpGet]
    public IEnumerable<UserEntity> GetUsers([FromServices]IUserRepository userRepository)
    {
        return userRepository.GetAll();
    }

    [Route("")]
    [HttpPost]
    public GenericCommandResult CreateUser([FromBody]CreateUserCommand command, [FromServices]UserHandler handler)
    {
        return (GenericCommandResult)handler.Handle(command);
    }

    [Route("")]
    [HttpDelete]
    public GenericCommandResult DeleteUser([FromBody] DeleteUserCommand command, [FromServices] UserHandler handler)
    {
        return (GenericCommandResult)handler.Handle(command);
    }
}
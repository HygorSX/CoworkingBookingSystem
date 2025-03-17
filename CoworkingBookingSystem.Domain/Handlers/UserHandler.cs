using CoworkingBookingSystem.Domain.Commands;
using CoworkingBookingSystem.Domain.Commands.Contracts;
using CoworkingBookingSystem.Domain.Commands.UserCommands;
using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Handlers.Contracts;
using CoworkingBookingSystem.Domain.Repositories;
using Flunt.Notifications;

namespace CoworkingBookingSystem.Domain.Handlers;

public class UserHandler : 
    Notifiable<Notification>,
    IHandler<CreateUserCommand>,
    IHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public ICommandResult Handle(CreateUserCommand command)
    {
        command.Validate();
        
        if(!command.IsValid)
            return new GenericCommandResult(false, "Ops, something went wrong!", command.Notifications);
        
        if(_userRepository.GetByEmail(command.Email) != null)
            return new GenericCommandResult(false, "Email already in use", null);
        
        var user = new UserEntity(command.Name, command.Email, command.Password, command.Type);
        
        _userRepository.Create(user);
        
        return new GenericCommandResult(true, "User created!", user);
    }

    public ICommandResult Handle(DeleteUserCommand command)
    {
        command.Validate();
        
        if(!command.IsValid)
            return new GenericCommandResult(false, "Ops, something went wrong!", command.Notifications);

        var user = _userRepository.GetById(command.UserId);
        
        if (user == null)
            return new GenericCommandResult(false, "User not found", null);
        
        _userRepository.Delete(user);
        
        return new GenericCommandResult(true, "User deleted!", user);
    }
}
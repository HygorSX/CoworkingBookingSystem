using CoworkingBookingSystem.Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace CoworkingBookingSystem.Domain.Commands.UserCommands;

public class DeleteUserCommand: Notifiable<Notification>, ICommand
{
    public Guid UserId { get; set; }
    
    public DeleteUserCommand() {}

    public DeleteUserCommand(Guid userId)
    {
        UserId = userId;
    }
    
    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .AreNotEquals(UserId, Guid.Empty, "UserId", "User ID is required")
        );
    }
}
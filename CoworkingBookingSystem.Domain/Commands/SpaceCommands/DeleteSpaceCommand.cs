using CoworkingBookingSystem.Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace CoworkingBookingSystem.Domain.Commands.SpaceCommands;

public class DeleteSpaceCommand : Notifiable<Notification>, ICommand
{
    public Guid SpaceId { get; set; }
    
    public DeleteSpaceCommand() {}

    public DeleteSpaceCommand(Guid spaceId)
    {   
        SpaceId = spaceId;  
    }

    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .AreNotEquals(SpaceId, Guid.Empty, "SpaceId", "Space ID is required")
        );
    }
}
using CoworkingBookingSystem.Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace CoworkingBookingSystem.Domain.Commands.SpaceCommands;

public class UpdateSpaceCommand : Notifiable<Notification>, ICommand
{
    public string Name { get; set; }
    public Guid SpaceId { get; set; }
    
    public UpdateSpaceCommand() {}

    public UpdateSpaceCommand(string name, Guid spaceId)
    {
        Name = name;
        SpaceId = spaceId;
    }

    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Name, "Name", "Name cannot be empty.")
                .AreNotEquals(SpaceId, Guid.Empty, "SpaceId", "Space ID is required"));
    }
}
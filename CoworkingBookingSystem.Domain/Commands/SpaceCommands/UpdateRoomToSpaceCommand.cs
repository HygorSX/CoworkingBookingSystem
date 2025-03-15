using Flunt.Notifications;
using Flunt.Validations;
using ICommand = CoworkingBookingSystem.Domain.Commands.Contracts.ICommand;

namespace CoworkingBookingSystem.Domain.Commands.SpaceCommands;

public class UpdateRoomToSpaceCommand : Notifiable<Notification>, ICommand
{
    public Guid SpaceId { get; set; }
    public Guid RoomId { get; set; }
    public string NewName { get; set; }

    public UpdateRoomToSpaceCommand(Guid spaceId, Guid roomId, string newName)
    {
        SpaceId = spaceId;
        RoomId = roomId;
        NewName = newName;
    }

    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .AreNotEquals(SpaceId, Guid.Empty, "SpaceId", "Space ID is required")
                .AreNotEquals(RoomId, Guid.Empty, "RoomId", "Room ID is required")
                .IsNotNullOrEmpty(NewName, "NewName","Room name cannot be empty.")
        );
    }
}
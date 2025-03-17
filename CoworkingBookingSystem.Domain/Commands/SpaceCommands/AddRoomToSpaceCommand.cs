using CoworkingBookingSystem.Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace CoworkingBookingSystem.Domain.Commands.SpaceCommands;

public class AddRoomToSpaceCommand : Notifiable<Notification>, ICommand
{
    public Guid SpaceId { get; set; }
    public string RoomName { get; set; }

    public AddRoomToSpaceCommand() {}
    
    public AddRoomToSpaceCommand(Guid spaceId, string roomName)
    {
        SpaceId = spaceId;
        RoomName = roomName;
    }

    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .AreNotEquals(SpaceId, Guid.Empty, "SpaceId", "Invalid SpaceId.")
                .IsNotNullOrWhiteSpace(RoomName, "RoomName", "Room name cannot be empty.")
        );
    }
}
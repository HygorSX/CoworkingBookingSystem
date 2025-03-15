using CoworkingBookingSystem.Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace CoworkingBookingSystem.Domain.Commands.SpaceCommands;

public class RemoveRoomFromSpaceCommand : Notifiable<Notification>, ICommand
{
    public Guid SpaceId { get; set; }
    public Guid RoomId { get; set; }

    public RemoveRoomFromSpaceCommand(Guid spaceId, Guid roomId)
    {
        SpaceId = spaceId;
        RoomId = roomId;
    }

    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .AreNotEquals(SpaceId, Guid.Empty, "SpaceId", "Space ID is required")
                .AreNotEquals(RoomId, Guid.Empty, "RoomId", "Room ID is required")
        );
    }
}
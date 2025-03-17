using CoworkingBookingSystem.Domain.Commands.Contracts;
using CoworkingBookingSystem.Domain.Entities.Enum;
using Flunt.Notifications;
using Flunt.Validations;

namespace CoworkingBookingSystem.Domain.Commands.ReservationCommands;

public class CreateReservationCommand : Notifiable<Notification>, ICommand                         
{
    public Guid UserId { get; set; }
    public Guid RoomId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public EReservationStatus Status { get; set; }

    public CreateReservationCommand(Guid userId, Guid roomId, DateTime startTime, DateTime endTime)
    {
        UserId = userId;
        RoomId = roomId;
        StartTime = startTime;
        EndTime = endTime;
    }


    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .AreNotEquals(UserId, Guid.Empty, "UserId", "User ID is required")
                .AreNotEquals(RoomId, Guid.Empty, "RoomId", "Room ID is required")
                .IsGreaterThan(EndTime, StartTime, "EndTime", "End time must be after start time")
        );
    }
}
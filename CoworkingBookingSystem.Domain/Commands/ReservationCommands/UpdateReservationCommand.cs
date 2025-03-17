using CoworkingBookingSystem.Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace CoworkingBookingSystem.Domain.Commands.ReservationCommands;

public class UpdateReservationTimeCommand : Notifiable<Notification>, ICommand
{
    public Guid ReservationId { get; set; }
    public Guid UserId { get; set; }
    public DateTime NewStartTime { get; set; }
    public DateTime NewEndTime { get; set; }

    public UpdateReservationTimeCommand(Guid userId, Guid reservationId, DateTime newStartTime, DateTime newEndTime)
    {
        UserId = userId;
        ReservationId = reservationId;
        NewStartTime = newStartTime;
        NewEndTime = newEndTime;
    }

    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .AreNotEquals(ReservationId, Guid.Empty, "ReservationId", "Reservation ID is required")
                .IsGreaterThan(NewEndTime, NewStartTime, "NewEndTime", "End time must be after start time")
                .IsGreaterOrEqualsThan(NewStartTime, DateTime.Now, "NewStartTime", "Start time cannot be in the past")
        );
    }
}
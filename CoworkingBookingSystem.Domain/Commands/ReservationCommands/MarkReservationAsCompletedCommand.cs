using CoworkingBookingSystem.Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace CoworkingBookingSystem.Domain.Commands.ReservationCommands;

public class MarkReservationAsCompletedCommand : Notifiable<Notification>, ICommand
{
    public Guid ReservationId { get; set; }
    public Guid UserId { get; set; }

    public MarkReservationAsCompletedCommand(Guid reservationId, Guid userId)
    {
        ReservationId = reservationId;
        UserId = userId;
    }

    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .AreNotEquals(ReservationId, Guid.Empty, "ReservationId", "Reservation ID is required")
        );
    }
}
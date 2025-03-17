using CoworkingBookingSystem.Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace CoworkingBookingSystem.Domain.Commands.ReservationCommands;

public class MarkReservationAsCanceledCommand : Notifiable<Notification>, ICommand
{
    public Guid UserId { get; set; }
    public Guid ReservationId { get; set; }

    public MarkReservationAsCanceledCommand(Guid userId, Guid reservationId)
    {
        UserId = userId;
        ReservationId = reservationId;
    }

    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .AreNotEquals(UserId, Guid.Empty, "UserId", "User ID is required")
                .AreNotEquals(ReservationId, Guid.Empty, "ReservationId", "Reservation ID is required")
        );
    }
}
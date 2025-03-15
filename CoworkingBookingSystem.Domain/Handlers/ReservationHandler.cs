using CoworkingBookingSystem.Domain.Commands;
using CoworkingBookingSystem.Domain.Commands.Contracts;
using CoworkingBookingSystem.Domain.Commands.ReservationCommands;
using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Handlers.Contracts;
using CoworkingBookingSystem.Domain.Repositories;
using Flunt.Notifications;

namespace CoworkingBookingSystem.Domain.Handlers;

public class ReservationHandler : 
    Notifiable<Notification>,
    IHandler<CreateReservation>,
    IHandler<MarkReservationAsReserved>,
    IHandler<MarkReservationAsCanceled>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IReservationRepository _reservationRepository;

    public ReservationHandler(IUserRepository userRepository, IRoomRepository roomRepository, IReservationRepository reservationRepository)
    {
        _userRepository = userRepository;
        _roomRepository = roomRepository;
        _reservationRepository = reservationRepository;
    }

    public ICommandResult Handle(CreateReservation command)
    {
        command.Validate();

        if (!command.IsValid)
            return new GenericCommandResult(false, "Ops, something went wrong!", command.Notifications);
        
        var user = _userRepository.GetById(command.UserId);
        
        if(user == null)
            return new GenericCommandResult(false, "User not found", null);
        
        var room = _roomRepository.GetById(command.RoomId);
        
        if(room == null)
            return new GenericCommandResult(false, "Room not found", null);
        
        if (_reservationRepository.HasConflict(command.RoomId, command.StartTime, command.EndTime))
            return new GenericCommandResult(false, "This room is already booked for this time", null);
        
        var reservation = new ReservationEntity(command.UserId, room.SpaceId, command.RoomId, command.StartTime, command.EndTime);
        
        _reservationRepository.CreateReservation(reservation);
        
        return new GenericCommandResult(true, "Reservation created", reservation);
    }

    public ICommandResult Handle(MarkReservationAsCanceled command)
    {
        command.Validate();

        if (!command.IsValid)
            return new GenericCommandResult(false, "Ops, something went wrong!", command.Notifications);
        
        var user = _userRepository.GetById(command.UserId);
        
        if(user == null)
            return new GenericCommandResult(false, "User not found", null);
        
        var reservation = _reservationRepository.GetReservationForUserById(command.ReservationId, command.UserId);
        
        if (reservation == null)
            return new GenericCommandResult(false, "Reservation not found", null);

        try
        {
            reservation.Cancel();
            
            _reservationRepository.UpdateReservation(reservation);
            
            return new GenericCommandResult(true, "Reservation canceled successfully", reservation);
        }
        catch (Exception ex)
        {
            return new GenericCommandResult(true, ex.Message, reservation);
        }
    }

    public ICommandResult Handle(MarkReservationAsReserved command)
    {
        command.Validate();

        if (!command.IsValid)
            return new GenericCommandResult(false, "Ops, something went wrong!", command.Notifications);
        
        var user = _userRepository.GetById(command.UserId);
        
        if(user == null)
            return new GenericCommandResult(false, "User not found", null);
        
        var reservation = _reservationRepository.GetReservationForUserById(command.ReservationId, command.UserId);
        
        if (reservation == null)
            return new GenericCommandResult(false, "Reservation not found", null);

        try
        {
            reservation.Confirm();
            
            _reservationRepository.UpdateReservation(reservation);
            
            return new GenericCommandResult(true, "Reservation confirmed successfully", reservation);
        }
        catch (Exception ex)
        {
            return new GenericCommandResult(true, ex.Message, reservation);
        }
    }
}
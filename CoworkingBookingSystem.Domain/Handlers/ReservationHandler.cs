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
    IHandler<CreateReservationCommand>,
    IHandler<UpdateReservationTimeCommand>,
    IHandler<MarkReservationAsReservedCommand>,
    IHandler<MarkReservationAsCanceledCommand>,
    IHandler<MarkReservationAsCompletedCommand>
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

    public ICommandResult Handle(CreateReservationCommand command)
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
        
        var existingReservations = _reservationRepository.GetConflictingReservationsForRoom(command.RoomId, command.StartTime, command.EndTime);
        
        if (existingReservations.Any())
            return new GenericCommandResult(false, "This room is already booked for this time", null);
        
        var reservation = new ReservationEntity(command.UserId, room.SpaceId, command.RoomId, command.StartTime, command.EndTime);
        
        _reservationRepository.CreateReservation(reservation);
        
        return new GenericCommandResult(true, "Reservation created", reservation);
    }
    
    public ICommandResult Handle(UpdateReservationTimeCommand command)
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

        var existingReservations = _reservationRepository.GetConflictingReservationsForRoom(reservation.RoomId, command.NewStartTime, command.NewEndTime);
        
        if (existingReservations.Any())
            return new GenericCommandResult(false, "This room is already booked for this time", null);

        reservation.UpdateTime(command.NewStartTime, command.NewEndTime);
        
        _reservationRepository.UpdateReservation(reservation);
    
        return new GenericCommandResult(true, "Reservation updated successfully", reservation);
    }

    public ICommandResult Handle(MarkReservationAsCanceledCommand command)
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

    public ICommandResult Handle(MarkReservationAsReservedCommand command)
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
    
    public ICommandResult Handle(MarkReservationAsCompletedCommand command)
    {
        command.Validate();

        if (!command.IsValid)
            return new GenericCommandResult(false, "Ops, something went wrong!", command.Notifications);

        var user = _userRepository.GetById(command.UserId);

        if (user == null)
            return new GenericCommandResult(false, "User not found", null);

        var reservation = _reservationRepository.GetReservationForUserById(command.ReservationId, command.UserId);

        if (reservation == null)
            return new GenericCommandResult(false, "Reservation not found", null);

        try
        {
            reservation.Conclued();
            _reservationRepository.UpdateReservation(reservation);
        
            return new GenericCommandResult(true, "Reservation marked as completed successfully", reservation);
        }
        catch (Exception ex)
        {
            return new GenericCommandResult(false, ex.Message, null);
        }
    }
}
using CoworkingBookingSystem.Domain.Commands;
using CoworkingBookingSystem.Domain.Commands.ReservationCommands;
using CoworkingBookingSystem.Domain.Commands.UserCommands;
using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Entities.Enum;
using CoworkingBookingSystem.Domain.Handlers;
using CoworkingBookingSystem.Domain.Tests.Repositories;

namespace CoworkingBookingSystem.Domain.Tests.HandlerTests.ReservationHandlerTests;

[TestClass]
public sealed class CreateReservationHandlerTests
{
    private CreateReservationCommand _invalidCommand;
    private CreateReservationCommand _validCommand;
    private ReservationHandler _reservationHandler;
    private FakeUserRepository _userRepository;
    private FakeRoomRepository _roomRepository;
    private FakeSpaceRepository _spaceRepository;
    private FakeReservationRepository _reservationRepository;

    [TestInitialize]
    public void Setup()
    {
        _userRepository = new FakeUserRepository();
        _roomRepository = new FakeRoomRepository();
        _spaceRepository = new FakeSpaceRepository();
        _reservationRepository = new FakeReservationRepository();
        _reservationHandler = new ReservationHandler(_userRepository, _roomRepository, _reservationRepository);
        
        var userToCreateReservation = new UserEntity("user", "user@gmail.com", "user12345", EUserType.Common);
        _userRepository.Create(userToCreateReservation);
        
        var roomToCreateReservation = new RoomEntity("roomTest", Guid.NewGuid());
        _roomRepository.CreateRoom(roomToCreateReservation);
        
        _invalidCommand = new CreateReservationCommand(Guid.Empty, Guid.Empty, DateTime.UtcNow, DateTime.UtcNow.AddHours(-1));
        _validCommand = new CreateReservationCommand(userToCreateReservation.Id, roomToCreateReservation.Id, DateTime.UtcNow.AddHours(3), DateTime.UtcNow.AddHours(6));
    }

    [TestMethod]
    public void Given_an_invalid_command_it_should_stop_the_application()
    {
        var result = (GenericCommandResult)_reservationHandler.Handle(_invalidCommand);
        Assert.IsFalse(result.Success, "The reservation creation should fail for an invalid command.");
    }

    [TestMethod]
    public void Given_a_valid_command_but_nonexistent_user_it_should_fail()
    {
        var command = new CreateReservationCommand(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddHours(3), DateTime.UtcNow.AddHours(6));
        var result = (GenericCommandResult)_reservationHandler.Handle(command);

        Assert.IsFalse(result.Success, "Reservation creation should fail when user is not found.");
        Assert.AreEqual("User not found", result.Message);
    }

    [TestMethod]
    public void Given_a_valid_command_but_nonexistent_room_it_should_fail()
    {
        var user = new UserEntity("user", "user@gmail.com", "user12345", EUserType.Common);
        _userRepository.Create(user);

        var command = new CreateReservationCommand(user.Id, Guid.NewGuid(), DateTime.UtcNow.AddHours(3), DateTime.UtcNow.AddHours(6));
        var result = (GenericCommandResult)_reservationHandler.Handle(command);

        Assert.IsFalse(result.Success, "Reservation creation should fail when room is not found.");
        Assert.AreEqual("Room not found", result.Message);
    }

    [TestMethod]
    public void Given_a_valid_command_but_conflicting_reservation_it_should_fail()
    {
        var user = new UserEntity("user", "user@gmail.com", "user12345", EUserType.Common);
        _userRepository.Create(user);

        var room = new RoomEntity("Room A", Guid.NewGuid());
        _roomRepository.CreateRoom(room);

        var existingReservation = new ReservationEntity(user.Id, room.SpaceId, room.Id, DateTime.UtcNow.AddHours(3), DateTime.UtcNow.AddHours(6));
        _reservationRepository.CreateReservation(existingReservation);

        var command = new CreateReservationCommand(user.Id, room.Id, DateTime.Now.AddHours(3), DateTime.UtcNow.AddHours(6));
        var result = (GenericCommandResult)_reservationHandler.Handle(command);

        Assert.IsFalse(result.Success, "Reservation creation should fail when there is a scheduling conflict.");
        Assert.AreEqual("This room is already booked for this time", result.Message);
    }
}
using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Entities.Enum;

namespace CoworkingBookingSystem.Domain.Tests.EntityTests;

[TestClass]
public class UserEntityTests
{
    private UserEntity _user;

    [TestInitialize]
    public void Setup()
    {
        _user = new UserEntity("user", "user@gmail.com", "user12345", EUserType.Common);
    }
    
    [TestMethod]
    public void Should_Create_User_With_Hashed_Password()
    {
        Assert.IsNotNull(_user.PasswordHash, "PasswordHash should not be null.");
        Assert.IsNotNull(_user.PasswordSalt, "PasswordSalt should not be null.");
    }

    [TestMethod]
    public void Should_Return_True_For_Correct_Password()
    {
        bool result = _user.CheckPassword("user12345");
        Assert.IsTrue(result, "CheckPassword should return true for the correct password.");
    }
    
    [TestMethod]
    public void Should_Return_False_For_Incorrect_Password()
    {
        bool result = _user.CheckPassword("WrongPassword");
        Assert.IsFalse(result, "CheckPassword should return false for an incorrect password.");
    }

    [TestMethod]
    public void Should_Add_Reservation_When_Limit_Not_Reached()
    {
        DateTime baseTime = DateTime.UtcNow.Date.AddHours(10);

        var reservation = new ReservationEntity(_user.Id, Guid.NewGuid(), Guid.NewGuid(), baseTime, baseTime.AddHours(1));
        _user.AddReservation(reservation);

        Assert.AreEqual(1, _user.Reservations.Count, "User should have 1 reservation.");
    }

    [TestMethod]
    public void Should_Throw_Exception_When_Max_Reservations_Reached()
    {
        DateTime baseTime = DateTime.UtcNow.Date.AddHours(9);

        _user.AddReservation(new ReservationEntity(_user.Id, Guid.NewGuid(), Guid.NewGuid(), baseTime, baseTime.AddHours(1)));
        _user.AddReservation(new ReservationEntity(_user.Id, Guid.NewGuid(), Guid.NewGuid(), baseTime.AddHours(2), baseTime.AddHours(3)));
        _user.AddReservation(new ReservationEntity(_user.Id, Guid.NewGuid(), Guid.NewGuid(), baseTime.AddHours(4), baseTime.AddHours(5)));

        var fourthReservation = new ReservationEntity(_user.Id, Guid.NewGuid(), Guid.NewGuid(), baseTime.AddHours(6), baseTime.AddHours(7));

        var exception = Assert.ThrowsException<InvalidOperationException>(() => _user.AddReservation(fourthReservation));
        Assert.AreEqual("User has reached the maximum number of reservations for this day.", exception.Message);
    }
}
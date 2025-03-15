using CoworkingBookingSystem.Domain.Commands.ReservationCommands;
using CoworkingBookingSystem.Domain.Entities.Enum;

namespace CoworkingBookingSystem.Domain.Tests.CommandTests.ReservationCommandTests;

[TestClass]
public sealed class CreateReservationCommandTests
{
    private CreateReservation _invalidCommand;
    private CreateReservation _validCommand;

    [TestInitialize]
    public void Setup()
    {
        _invalidCommand = new CreateReservation(Guid.Empty, Guid.Empty, DateTime.Now, DateTime.Now.AddHours(-1), EReservationStatus.Pending);
        _validCommand = new CreateReservation(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, DateTime.Now.AddHours(2), EReservationStatus.Pending);
    }

    [TestMethod]
    public void Given_an_invalid_command_it_should_fail_validation()
    {
        _invalidCommand.Validate();
        Assert.IsFalse(_invalidCommand.IsValid, "Validation should fail due to invalid data.");
    }

    [TestMethod]
    public void Given_a_valid_command_it_should_pass_validation()
    {
        _validCommand.Validate();
        Assert.IsTrue(_validCommand.IsValid, "Validation should pass for a correctly filled command.");
    }
}
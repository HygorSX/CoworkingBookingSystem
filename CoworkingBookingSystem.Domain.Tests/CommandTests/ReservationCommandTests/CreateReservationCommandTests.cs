using CoworkingBookingSystem.Domain.Commands.ReservationCommands;
using CoworkingBookingSystem.Domain.Entities.Enum;

namespace CoworkingBookingSystem.Domain.Tests.CommandTests.ReservationCommandTests;

[TestClass]
public sealed class CreateReservationCommandTests
{
    private CreateReservationCommand _invalidCommand;
    private CreateReservationCommand _validCommand;

    [TestInitialize]
    public void Setup()
    {
        _invalidCommand = new CreateReservationCommand(Guid.Empty, Guid.Empty, DateTime.UtcNow, DateTime.UtcNow.AddHours(-3));
        _validCommand = new CreateReservationCommand(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddHours(6));
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
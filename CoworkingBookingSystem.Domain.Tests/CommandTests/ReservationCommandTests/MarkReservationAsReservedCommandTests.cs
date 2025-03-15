using CoworkingBookingSystem.Domain.Commands.ReservationCommands;

namespace CoworkingBookingSystem.Domain.Tests.CommandTests.ReservationCommandTests;

[TestClass]
public sealed class MarkReservationAsReservedCommandTests
{
    private MarkReservationAsReserved _invalidCommand;
    private MarkReservationAsReserved _validCommand;

    [TestInitialize]
    public void Setup()
    {
        _invalidCommand = new MarkReservationAsReserved(Guid.Empty, Guid.Empty);
        _validCommand = new MarkReservationAsReserved(Guid.NewGuid(), Guid.NewGuid());
    }

    [TestMethod]
    public void Given_an_invalid_command_it_should_fail_validation()
    {
        _invalidCommand.Validate();
        Assert.IsFalse(_invalidCommand.IsValid, "Validation should fail due to missing user and reservation IDs.");
    }

    [TestMethod]
    public void Given_a_valid_command_it_should_pass_validation()
    {
        _validCommand.Validate();
        Assert.IsTrue(_validCommand.IsValid, "Validation should pass for a correctly filled command.");
    }
}
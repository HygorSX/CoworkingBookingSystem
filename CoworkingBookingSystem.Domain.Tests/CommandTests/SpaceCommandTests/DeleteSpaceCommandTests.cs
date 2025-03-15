using CoworkingBookingSystem.Domain.Commands.SpaceCommands;

namespace CoworkingBookingSystem.Domain.Tests.CommandTests.SpaceCommandTests;

[TestClass]
public sealed class DeleteSpaceCommandTests
{
    private DeleteSpaceCommand _invalidCommand;
    private DeleteSpaceCommand _validCommand;
    
    [TestInitialize]
    public void Setup()
    {
        _invalidCommand = new DeleteSpaceCommand(Guid.Empty);
        _validCommand = new DeleteSpaceCommand(Guid.NewGuid());
    }

    [TestMethod]
    public void Should_return_invalid_when_space_id_is_empty()
    {
        _invalidCommand.Validate();
        Assert.IsFalse(_invalidCommand.IsValid);
    }

    [TestMethod]
    public void Should_return_valid_when_space_id_is_valid()
    {
        _validCommand.Validate();
        Assert.IsTrue(_validCommand.IsValid);
    }
}
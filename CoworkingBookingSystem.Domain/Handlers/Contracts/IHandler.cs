using CoworkingBookingSystem.Domain.Commands.Contracts;

namespace CoworkingBookingSystem.Domain.Handlers.Contracts;

public interface IHandler<T> where T : ICommand
{
    ICommandResult Handle(T command);
}
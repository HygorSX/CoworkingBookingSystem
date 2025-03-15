namespace CoworkingBookingSystem.Domain.Commands.Contracts;

public interface ICommand
{
    public void Validate();
}
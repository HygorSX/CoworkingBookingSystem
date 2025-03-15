using CoworkingBookingSystem.Domain.Commands.Contracts;
using CoworkingBookingSystem.Domain.Entities.Enum;
using Flunt.Notifications;
using Flunt.Validations;

namespace CoworkingBookingSystem.Domain.Commands.UserCommands;

public class CreateUserCommand : Notifiable<Notification>, ICommand
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public EUserType Type { get; set; }

    public CreateUserCommand() { }
    
    public CreateUserCommand(string name, string email, string password, EUserType type)
    {
        Name = name;
        Email = email;
        Password = password;
        Type = type;
    }
    
    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Name, "Name", "Name is required")
                .IsEmail(Email, "Email", "Invalid email")
                .IsGreaterThan(Password, 5, "Password", "Password must have at least 6 characters")
        );
    }
}
using CoworkingBookingSystem.Domain.Commands.Contracts;
using CoworkingBookingSystem.Domain.Entities;
using Flunt.Notifications;
using Flunt.Validations;

namespace CoworkingBookingSystem.Domain.Commands.SpaceCommands;

public class CreateSpaceCommand : Notifiable<Notification>, ICommand
{
    public string Name { get; set; }
    public List<string> RoomNames { get; set; }

    public CreateSpaceCommand() { }
    
    public CreateSpaceCommand(string name, List<string> roomNames)
    {
        Name = name;
        RoomNames = roomNames ?? new List<string>();
    }

    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Name, "Name","Space name cannot be empty."));
    }
}
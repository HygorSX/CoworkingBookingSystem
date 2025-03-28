using CoworkingBookingSystem.Domain.Commands;
using CoworkingBookingSystem.Domain.Commands.Contracts;
using CoworkingBookingSystem.Domain.Commands.SpaceCommands;
using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Handlers.Contracts;
using CoworkingBookingSystem.Domain.Repositories;
using Flunt.Notifications;

namespace CoworkingBookingSystem.Domain.Handlers;

public class SpaceHandler : 
    Notifiable<Notification>, 
    IHandler<CreateSpaceCommand>,
    IHandler<DeleteSpaceCommand>,
    IHandler<UpdateSpaceCommand>,
    IHandler<AddRoomToSpaceCommand>,
    IHandler<RemoveRoomFromSpaceCommand>,
    IHandler<UpdateRoomToSpaceCommand>
{
    private readonly ISpaceRepository _spaceRepository;

    public SpaceHandler(ISpaceRepository spaceRepository)
    {
        _spaceRepository = spaceRepository;
    }

    public ICommandResult Handle(CreateSpaceCommand command)
    {
        command.Validate();

        if (!command.IsValid)
            return new GenericCommandResult(false, "Ops, something went wrong!", command.Notifications);

        if(_spaceRepository.GetSpaceByName(command.Name) != null)
            return new GenericCommandResult(false, "Space already exists", null);
        
        var space = new SpaceEntity(command.Name);
        
        foreach (var roomName in command.RoomNames)
        {
            var room = new RoomEntity(roomName, space.Id);
            space.AddRoom(room);
        }
        
        _spaceRepository.CreateSpace(space);
        
        return new GenericCommandResult(true, "Space created!", space);
    }

    public ICommandResult Handle(UpdateSpaceCommand command)
    {
        command.Validate();

        if (!command.IsValid)
            return new GenericCommandResult(false, "Ops, something went wrong!", command.Notifications);
        
        var space = _spaceRepository.GetSpaceById(command.SpaceId);
        
        if(_spaceRepository.GetSpaceByName(command.Name) != null)
            return new GenericCommandResult(false, "There is already a space with this name", null);
        
        if (space == null)
            return new GenericCommandResult(false, "Space not found", null);
        
        if (space.Name == command.Name)
            return new GenericCommandResult(false, "No changes detected", space);
        
        space.UpdateName(command.Name);
        
        _spaceRepository.UpdateSpace(space);
        
        return new GenericCommandResult(true, "Space updated!", space);
    }
    
    public ICommandResult Handle(DeleteSpaceCommand command)
    {
        command.Validate();
        
        if(!command.IsValid)
            return new GenericCommandResult(false, "Ops, something went wrong!", command.Notifications);

        var space = _spaceRepository.GetSpaceById(command.SpaceId);
        
        if (space == null)
            return new GenericCommandResult(false, "Space not found", null);
        
        if (space.Rooms.Any())
            return new GenericCommandResult(false, "Cannot delete a space with existing rooms", null);
        
        _spaceRepository.DeleteSpace(space);
        
        return new GenericCommandResult(true, "Space deleted!", space);
    }

    public ICommandResult Handle(AddRoomToSpaceCommand command)
    {
        command.Validate();
        
        if(!command.IsValid)
            return new GenericCommandResult(false, "Ops, something went wrong!", command.Notifications);
        
        var space = _spaceRepository.GetSpaceById(command.SpaceId);
        
        if (space == null)
            return new GenericCommandResult(false, "Space not found", null);
        
        if (space.Rooms.Any(r => r.Name == command.RoomName))
            return new GenericCommandResult(false, "Room with this name already exists", null);
        
        var room = new RoomEntity(command.RoomName, space.Id);
        
        space.AddRoom(room);
        
        _spaceRepository.UpdateSpace(space);
        
        return new GenericCommandResult(true, "Rooms added to space successfully!", space);
    }


    public ICommandResult Handle(RemoveRoomFromSpaceCommand command)
    {
        command.Validate();
        
        if(!command.IsValid)
            return new GenericCommandResult(false, "Ops, something went wrong!", command.Notifications);
        
        var space = _spaceRepository.GetSpaceById(command.SpaceId);
        
        if (space == null)
            return new GenericCommandResult(false, "Space not found", null);
        
        var room = space.Rooms.FirstOrDefault(r => r.Id == command.RoomId);
        
        if (room == null)
            return new GenericCommandResult(false, "Room not found", null);
        
        space.RemoveRoom(room);
        
        _spaceRepository.UpdateSpace(space);
        
        return new GenericCommandResult(true, "Rooms removed to space successfully!", space);
    }

    public ICommandResult Handle(UpdateRoomToSpaceCommand command)
    {
        command.Validate();
        
        if(!command.IsValid)
            return new GenericCommandResult(false, "Ops, something went wrong!", command.Notifications);
        
        var space = _spaceRepository.GetSpaceById(command.SpaceId);
        
        if (space == null)
            return new GenericCommandResult(false, "Space not found", null);
        
        var room = space.Rooms.FirstOrDefault(r => r.Id == command.RoomId);

        if (room == null)
            return new GenericCommandResult(false, "Room not found", null);
        
        space.UpdateRoom(command.RoomId, command.NewName);
        
        _spaceRepository.UpdateSpace(space);
        
        return new GenericCommandResult(true, "Room updated successfully!", space);
    }
}
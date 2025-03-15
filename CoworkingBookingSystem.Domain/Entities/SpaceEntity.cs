using System.Collections.ObjectModel;

namespace CoworkingBookingSystem.Domain.Entities;

public class SpaceEntity : Entity
{
    public string Name { get; private set; }
    public ICollection<RoomEntity> Rooms { get; private set; } = new HashSet<RoomEntity>();

    protected SpaceEntity() {}
    
    public SpaceEntity(string name)
    {
        Name = name;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
    
    public void AddRoom(RoomEntity room)
    {
        if(room == null)
            throw new ArgumentNullException("The room cannot be null");
        
        if (Rooms.Contains(room))
            throw new InvalidOperationException("This room is already added to the space.");

        Rooms.Add(room);
    }

    public void UpdateRoom(Guid roomId, string newName)
    {
        var room = Rooms.FirstOrDefault(r => r.Id == roomId);
        
        if(room == null)
            throw new ArgumentNullException("Room not found.");
        
        room.UpdateName(newName);
    }
    
    public void RemoveRoom(RoomEntity room)
    {
        if (room == null)
            throw new ArgumentNullException(nameof(room), "Room not found.");
        
        Rooms.Remove(room);
    }
}
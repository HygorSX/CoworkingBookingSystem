using System.Text.Json.Serialization;

namespace CoworkingBookingSystem.Domain.Entities;

public class RoomEntity : Entity
{
    public string Name { get; private set; }
    public Guid SpaceId { get; private set; }
    
    [JsonIgnore]
    public SpaceEntity Space { get; private set; }

    protected RoomEntity() { }

    public RoomEntity(string name, Guid spaceId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Room name cannot be empty.");
        
        Name = name;
        SpaceId = spaceId;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Room name cannot be empty.");

        Name = name;
    }
}
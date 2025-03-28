using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Repositories;

namespace CoworkingBookingSystem.Domain.Tests.Repositories;

public class FakeSpaceRepository : ISpaceRepository
{
    private readonly List<SpaceEntity> _spaces = new List<SpaceEntity>();
    public void CreateSpace(SpaceEntity space)
    {
        _spaces.Add(space);
    }

    public void UpdateSpace(SpaceEntity space)
    {
        var existing = _spaces.FirstOrDefault(s => s.Id == space.Id);
        if (existing != null)
        {
            existing.UpdateName(space.Name);
        }
    }

    public void DeleteSpace(SpaceEntity space)
    {
        _spaces.Remove(space);
    }

    public SpaceEntity GetSpaceById(Guid spaceId)
    {
        return _spaces.Find(r => r.Id == spaceId);
    }

    public SpaceEntity GetSpaceByName(string name)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<SpaceEntity> GetAllSpaces()
    {
        throw new NotImplementedException();
    }
}
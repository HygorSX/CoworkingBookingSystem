using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Infra.Contexts;
using CoworkingBookingSystem.Domain.Queries;
using CoworkingBookingSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoworkingBookingSystem.Domain.Infra.Repositories;

public class SpaceRepository : ISpaceRepository
{
    private readonly AppDbContext _db;

    public SpaceRepository(AppDbContext db)
    {
        _db = db;
    }

    public void CreateSpace(SpaceEntity space)
    {
        _db.Spaces.Add(space);
        _db.SaveChanges();
    }

    public void UpdateSpace(SpaceEntity space)
    {
        _db.Spaces.Update(space);
        _db.SaveChanges();
    }

    public void DeleteSpace(SpaceEntity space)
    {
        _db.Spaces.Remove(space);
        _db.SaveChanges();
    }

    public SpaceEntity GetSpaceById(Guid spaceId)
    {
        return _db.Spaces
            .Include(s => s.Rooms) 
            .Where(SpaceQueries.GetSpaceById(spaceId))
            .FirstOrDefault(s => s.Id == spaceId);
    }

    public SpaceEntity GetSpaceByName(string name)
    {
        return _db.Spaces
            .Include(s => s.Rooms) 
            .AsNoTracking()
            .Where(SpaceQueries.GetSpaceByName(name))
            .OrderBy(s => s.Name)
            .FirstOrDefault();
    }

    public IEnumerable<SpaceEntity> GetAllSpaces()
    {
        return _db.Spaces
            .Include(s => s.Rooms) 
            .AsNoTracking()
            .Where(SpaceQueries.GetAllSpaces())
            .OrderBy(s => s.Name);
    }
}
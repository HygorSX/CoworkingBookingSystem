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
        _db.Entry(space).State = EntityState.Modified;
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
                .AsNoTracking()
                .FirstOrDefault(s => s.Id == spaceId);
    }

    public IEnumerable<SpaceEntity> GetAllSpaces()
    {
        return _db.Spaces
            .AsNoTracking()
            .Where(SpaceQueries.GetAllSpaces())
            .OrderBy(s => s.Name);
    }
}
using System.Linq.Expressions;
using CoworkingBookingSystem.Domain.Entities;

namespace CoworkingBookingSystem.Domain.Queries;

public static class SpaceQueries
{
    public static Expression<Func<SpaceEntity, bool>> GetSpaceById(Guid id)
    {
        return x => x.Id == id;
    }

    public static Expression<Func<SpaceEntity, bool>> GetAllSpaces()
    {
        return space => true;
    }
}
using CoworkingBookingSystem.Domain.Entities;

namespace CoworkingBookingSystem.Domain.Repositories;

public interface ISpaceRepository
{
    void CreateSpace(SpaceEntity space);
    void UpdateSpace(SpaceEntity space);
    void DeleteSpace(SpaceEntity space);
    SpaceEntity GetSpaceById(Guid spaceId);
    List<SpaceEntity> GetAllSpaces();
}
using CoworkingBookingSystem.Domain.Entities;

namespace CoworkingBookingSystem.Domain.Repositories;

public interface IUserRepository
{
    UserEntity GetById(Guid userId);
    void Create(UserEntity user);
    void Delete(UserEntity user);
}
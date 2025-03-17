using CoworkingBookingSystem.Domain.Entities;

namespace CoworkingBookingSystem.Domain.Repositories;

public interface IUserRepository
{
    UserEntity GetById(Guid userId);
    UserEntity GetByEmail(string email);
    void Create(UserEntity user);
    void Delete(UserEntity user);
}
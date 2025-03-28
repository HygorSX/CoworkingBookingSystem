using CoworkingBookingSystem.Domain.Entities;

namespace CoworkingBookingSystem.Domain.Repositories;

public interface IUserRepository
{
    UserEntity GetById(Guid userId);
    UserEntity GetByEmail(string email);
    IEnumerable<UserEntity> GetAll();
    void Create(UserEntity user);
    void Delete(UserEntity user);
}
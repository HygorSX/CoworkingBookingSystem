using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Infra.Contexts;
using CoworkingBookingSystem.Domain.Queries;
using CoworkingBookingSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoworkingBookingSystem.Domain.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public UserEntity GetById(Guid userId)
    {
        return _db.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Id == userId);
    }

    public UserEntity GetByEmail(string email)
    {
        return _db.Users
            .AsNoTracking()
            .Where(UserQueries.GetByEmail(email))
            .FirstOrDefault();
    }

    public IEnumerable<UserEntity> GetAll()
    {
        return _db.Users
                .AsNoTracking()
                .Where(UserQueries.GetAll())
                .OrderBy(u => u.Name);
    }

    public void Create(UserEntity user)
    {
        _db.Users.Add(user);
        _db.SaveChanges();
    }

    public void Delete(UserEntity user)
    {
        _db.Users.Remove(user);
        _db.SaveChanges();
    }
}
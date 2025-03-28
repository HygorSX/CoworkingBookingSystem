using System.Linq.Expressions;
using CoworkingBookingSystem.Domain.Entities;

namespace CoworkingBookingSystem.Domain.Queries;

public static class UserQueries
{
    public static Expression<Func<UserEntity, bool>> GetByEmail(string email)
    {
        return u => u.Email == email;
    }

    public static Expression<Func<UserEntity, bool>> GetById(Guid userId)
    {
        return u => u.Id == userId;
    }

    public static Expression<Func<UserEntity, bool>> GetAll()
    {
        return u => true;
    }
}
﻿using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Handlers;
using CoworkingBookingSystem.Domain.Repositories;

namespace CoworkingBookingSystem.Domain.Tests.Repositories;

public class FakeUserRepository : IUserRepository
{
    private readonly List<UserEntity> _users = new List<UserEntity>();
    public UserEntity GetById(Guid userId)
    {
        return _users.FirstOrDefault(u => u.Id == userId);
    }

    public UserEntity GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserEntity> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Create(UserEntity user)
    {
        _users.Add(user);
    }

    public void Delete(UserEntity user)
    {
        _users.Remove(user);
    }
}
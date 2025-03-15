using System.Collections.ObjectModel;
using CoworkingBookingSystem.Domain.Entities.Enum;

namespace CoworkingBookingSystem.Domain.Entities;

public class UserEntity : Entity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public byte[] PasswordHash { get; private set; }
    public byte[] PasswordSalt { get; private set; }
    public EUserType Type { get; private set; }
    
    public ICollection<ReservationEntity> Reservations { get; private set; } = new Collection<ReservationEntity>();
    
    protected UserEntity() {}
    public UserEntity(string name, string email, string password, EUserType type)
    {
        Name = name;
        Email = email;
        Type = type;
        SetPassword(password);
    }

    public bool HasMaxReservationsForDay(DateTime date)
    {
        return Reservations.Count(r => r.StartTime.Date == date.Date) >= 3;
    }

    public void AddReservation(ReservationEntity reservation)
    {
        if (HasMaxReservationsForDay(reservation.StartTime.Date))
            throw new InvalidOperationException("User has reached the maximum number of reservations for this day.");
        
        Reservations.Add(reservation);
    }

    public void SetPassword(string password)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        PasswordSalt = hmac.Key;
        PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    public bool CheckPassword(string password)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(PasswordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return PasswordHash.SequenceEqual(computedHash);
    }
}
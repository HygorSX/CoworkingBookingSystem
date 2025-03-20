using CoworkingBookingSystem.Domain.Entities.Enum;

namespace CoworkingBookingSystem.Domain.Entities;

public class ReservationEntity : Entity
{
    public Guid UserId { get; private set; }
    public UserEntity User { get; private set; }

    public Guid SpaceId { get; private set; }
    public SpaceEntity Space { get; private set; }

    public Guid RoomId { get; private set; }
    public RoomEntity Room { get; private set; }

    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    public EReservationStatus Status { get; private set; }
    
    
    protected ReservationEntity() {}
    
    public ReservationEntity(Guid userId, Guid spaceId, Guid roomId, DateTime startTime, DateTime endTime)
    {
        if(endTime <= startTime)
            throw new ArgumentException("The end time must be greater than the start time.");
        
        if (startTime < DateTime.UtcNow)
            throw new ArgumentException("Reservations cannot be made in the past.");
        
        if (startTime.Date > DateTime.UtcNow.Date.AddDays(30))
            throw new ArgumentException("Reservations can only be made up to 30 days in advance.");
        
        UserId = userId;
        SpaceId = spaceId;
        RoomId = roomId;
        StartTime = startTime;
        EndTime = endTime;
        Status = EReservationStatus.Pending;
    }

    public void Confirm()
    {
        if(Status != EReservationStatus.Pending)
            throw new InvalidOperationException("Only pending reservations can be confirmed.");

        Status = EReservationStatus.Reserved;
    }

    public void Cancel()
    {
        if (Status == EReservationStatus.Conclued)
            throw new InvalidOperationException("Completed reservations cannot be canceled.");
        
        if (StartTime <= DateTime.UtcNow)
            throw new InvalidOperationException("Cannot cancel a reservation that has already started.");

        Status = EReservationStatus.Empty;
    }
    
    public void Conclued()
    {
        if (this.Status == EReservationStatus.Conclued)
            throw new InvalidOperationException("This reservation is already marked as completed.");

        this.Status = EReservationStatus.Conclued;
    }
    
    public bool ConflictsWith(List<ReservationEntity> existingReservations, DateTime startTime, DateTime endTime)
    {
        return existingReservations.Any(r => r.StartTime < endTime && r.EndTime > startTime);
    }
    
    public bool IsWithinAllowedPeriod()
    {
        return StartTime.Date >= DateTime.UtcNow.Date && StartTime.Date <= DateTime.UtcNow.Date.AddDays(30);
    }

    public void UpdateTime(DateTime commandNewStartTime, DateTime commandNewEndTime)
    {
        StartTime = commandNewStartTime;
        EndTime = commandNewEndTime;
    }
}
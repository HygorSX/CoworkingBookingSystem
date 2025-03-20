using CoworkingBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoworkingBookingSystem.Domain.Infra.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<SpaceEntity> Spaces { get; set; }
    public DbSet<RoomEntity> Rooms { get; set; }
    public DbSet<ReservationEntity> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);
        });
        
        modelBuilder.Entity<ReservationEntity>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(r => r.UserId)
                .IsRequired();

            entity.Property(r => r.RoomId)
                .IsRequired();

            entity.Property(r => r.StartTime)
                .IsRequired();

            entity.Property(r => r.EndTime)
                .IsRequired();

            entity.Property(r => r.Status)
                .IsRequired()
                .HasConversion<int>();

            entity.HasOne<UserEntity>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne<RoomEntity>()
                .WithMany()
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<RoomEntity>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(r => r.SpaceId)
                .IsRequired();

            entity.HasOne<SpaceEntity>()
                .WithMany()
                .HasForeignKey(r => r.SpaceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SpaceEntity>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);
        });
    }
}
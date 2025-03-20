using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Entities.Enum;
using CoworkingBookingSystem.Domain.Queries;

namespace CoworkingBookingSystem.Domain.Tests.Queries
{
    [TestClass]
    public class ReservationQueriesTests
    {
        private List<ReservationEntity> _reservations;
        private UserEntity _user;
        private RoomEntity _room;

        [TestInitialize]
        public void Setup()
        {
            _user = new UserEntity("userTest", "user@gmail.com", "user123", EUserType.Common);
            _room = new RoomEntity("roomTest", Guid.NewGuid());

            _reservations = new List<ReservationEntity>
            {
                new ReservationEntity(_user.Id, _room.SpaceId, _room.Id, DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2)),
                new ReservationEntity(_user.Id, _room.SpaceId, _room.Id, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(1).AddHours(2)),
                new ReservationEntity(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddHours(3), DateTime.UtcNow.AddHours(4))
            };
        }

        [TestMethod]
        public void GetReservationForUserById_ShouldReturnReservation_WhenExists()
        {
            var reservationToFind = _reservations[0];
            var query = ReservationQueries.GetReservationForUserById(reservationToFind.Id, _user.Id);
            var result = _reservations.AsQueryable().Where(query).FirstOrDefault();

            Assert.IsNotNull(result);
            Assert.AreEqual(reservationToFind.Id, result.Id);
            Assert.AreEqual(_user.Id, result.UserId);
        }

        [TestMethod]
        public void GetReservationForUserById_ShouldReturnNull_WhenReservationDoesNotExist()
        {
            var query = ReservationQueries.GetReservationForUserById(Guid.NewGuid(), _user.Id);
            var result = _reservations.AsQueryable().Where(query).FirstOrDefault();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetReservationsByUser_ShouldReturnAllUserReservations()
        {
            var query = ReservationQueries.GetReservationsByUser(_user.Id);
            var result = _reservations.AsQueryable().Where(query).ToList();

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(r => r.UserId == _user.Id));
        }

        [TestMethod]
        public void GetReservationsByUser_ShouldReturnEmpty_WhenUserHasNoReservations()
        {
            var query = ReservationQueries.GetReservationsByUser(Guid.NewGuid());
            var result = _reservations.AsQueryable().Where(query).ToList();

            Assert.AreEqual(0, result.Count);
        }

        public void GetFutureReservationsForRoom_ShouldReturnReservationsForGivenRoom()
        {
            var query = ReservationQueries.GetFutureReservationsForRoom(_room.Id);
            var result = _reservations.AsQueryable().Where(query).ToList();

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(r => r.RoomId == _room.Id));
        }

        [TestMethod]
        public void GetFutureReservationsForRoom_ShouldReturnEmpty_WhenNoReservationsForRoom()
        {
            var query = ReservationQueries.GetFutureReservationsForRoom(Guid.NewGuid());
            var result = _reservations.AsQueryable().Where(query).ToList();

            Assert.AreEqual(0, result.Count);
        }
    }
}
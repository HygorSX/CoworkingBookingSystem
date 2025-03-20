using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Queries;

namespace CoworkingBookingSystem.Domain.Tests.Queries
{
    [TestClass]
    public class RoomQueriesTests
    {
        private List<RoomEntity> _rooms;
        private SpaceEntity _space;

        [TestInitialize]
        public void Setup()
        {
            _space = new SpaceEntity("SpaceTest");
            
            _rooms = new List<RoomEntity>
            {
                new RoomEntity("Sala A", _space.Id),
                new RoomEntity("Sala B", _space.Id),
                new RoomEntity("Sala C", _space.Id)
            };
        }

        [TestMethod]
        public void GetRoomById_ShouldReturnRoom_WhenIdExists()
        {
            var roomToFind = _rooms[1];
            var query = RoomQueries.GetRoomById(roomToFind.Id);
            var result = _rooms.AsQueryable().Where(query).FirstOrDefault();

            Assert.IsNotNull(result);
            Assert.AreEqual(roomToFind.Id, result.Id);
        }

        [TestMethod]
        public void GetRoomById_ShouldReturnNull_WhenIdDoesNotExist()
        {
            var query = RoomQueries.GetRoomById(Guid.NewGuid());
            var result = _rooms.AsQueryable().Where(query).FirstOrDefault();

            Assert.IsNull(result);
        }
    }
}
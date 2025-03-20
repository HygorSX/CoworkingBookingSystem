using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Queries;

namespace CoworkingBookingSystem.Domain.Tests.Queries
{
    [TestClass]
    public class SpaceQueriesTests
    {
        private List<SpaceEntity> _spaces;

        [TestInitialize]
        public void Setup()
        {
            _spaces = new List<SpaceEntity>
            {
                new SpaceEntity("Sala de Reunião 1"),
                new SpaceEntity("Sala de Treinamento"),
                new SpaceEntity("Escritório Privado")
            };
        }

        [TestMethod]
        public void GetSpaceById_ShouldReturnSpace_WhenIdExists()
        {
            var spaceToFind = _spaces[1];
            var query = SpaceQueries.GetSpaceById(spaceToFind.Id);
            var result = _spaces.AsQueryable().Where(query).FirstOrDefault();

            Assert.IsNotNull(result);
            Assert.AreEqual(spaceToFind.Id, result.Id);
        }

        [TestMethod]
        public void GetSpaceById_ShouldReturnNull_WhenIdDoesNotExist()
        {
            var query = SpaceQueries.GetSpaceById(Guid.NewGuid());
            var result = _spaces.AsQueryable().Where(query).FirstOrDefault();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllSpaces_ShouldReturnAllSpaces()
        {
            var query = SpaceQueries.GetAllSpaces();
            var result = _spaces.AsQueryable().Where(query).ToList();

            Assert.AreEqual(_spaces.Count, result.Count);
        }
    }
}
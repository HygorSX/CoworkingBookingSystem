using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Entities.Enum;
using CoworkingBookingSystem.Domain.Queries;

namespace CoworkingBookingSystem.Domain.Tests.Queries
{
    [TestClass]
    public class UserQueriesTests
    {
        private List<UserEntity> _users;

        [TestInitialize]
        public void Setup()
        {
            _users = new List<UserEntity>
            {
                new UserEntity("John Doe", "john.doe@example.com", "john123", EUserType.Common),
                new UserEntity("Jane Doe", "jane.doe@example.com","jane123", EUserType.Common),
                new UserEntity("Alice Smith", "alice.smith@example.com", "alice123", EUserType.Common)
            };
        }

        [TestMethod]
        public void GetByEmail_ShouldReturnUser_WhenEmailExists()
        {
            var emailToFind = "jane.doe@example.com";
            var query = UserQueries.GetByEmail(emailToFind);
            var result = _users.AsQueryable().Where(query).FirstOrDefault();

            Assert.IsNotNull(result);
            Assert.AreEqual(emailToFind, result.Email);
        }

        [TestMethod]
        public void GetByEmail_ShouldReturnNull_WhenEmailDoesNotExist()
        {
            var emailToFind = "nonexistent@example.com";
            var query = UserQueries.GetByEmail(emailToFind);
            var result = _users.AsQueryable().Where(query).FirstOrDefault();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetById_ShouldReturnUser_WhenIdExists()
        {
            var userToFind = _users[1];
            var query = UserQueries.GetById(userToFind.Id);
            var result = _users.AsQueryable().Where(query).FirstOrDefault();

            Assert.IsNotNull(result);
            Assert.AreEqual(userToFind.Id, result.Id);
        }

        [TestMethod]
        public void GetById_ShouldReturnNull_WhenIdDoesNotExist()
        {
            var query = UserQueries.GetById(Guid.NewGuid());
            var result = _users.AsQueryable().Where(query).FirstOrDefault();

            Assert.IsNull(result);
        }
    }
}
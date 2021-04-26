using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using SecretSanta.Api.Controllers;
using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTests
    {
        [TestMethod]
        public void Constructor_WithNullUserRepository_ThrowsAppropriateException()
        {
            ArgumentNullException ex = Assert.ThrowsException<ArgumentNullException>(
                () => new UsersController(null!));
        }

        [TestMethod]
        public void Get_WithData_ReturnsUsers()
        {
            //Arrange
            UsersController controller = new(new UserRepository());

            //Act
            IEnumerable<User> users = controller.Get();

            //Assert
            Assert.IsTrue(users.Any());
        }
        
        [TestMethod]
        [DataRow(123)]
        [DataRow(89)]
        [DataRow(0)]
        public void Get_WithId_ReturnsUserUserRepository(int id)
        {
            //Arrange
            TestableUserRepository repository = new();
            UsersController controller = new(repository);
            User expectedUser = new();
            repository.GetItemUser = expectedUser;

            //Act
            ActionResult<User?> result = controller.Get(id);

            //Assert
            Assert.AreEqual(id, repository.GetItemId);
            Assert.AreEqual(expectedUser, result.Value);
        }

       [TestMethod]
        public void Get_WithNegativeId_ReturnsNotFound()
        {
            //Arrange
            TestableUserRepository repository = new();
            UsersController controller = new(repository);
            User expectedUser = new();
            repository.GetItemUser = expectedUser;

            //Act
            ActionResult<User?> result = controller.Get(-1);

            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
        }

        private class TestableUserRepository : IUserRepository
        {
            public User Create(User item)
            {
                throw new System.NotImplementedException();
            }

            public User? GetItemUser { get; set; }
            public int GetItemId { get; set; }
            public User? GetItem(int id)
            {
                GetItemId = id;
                return GetItemUser;
            }

            public ICollection<User> List()
            {
                throw new System.NotImplementedException();
            }

            public bool Remove(int id)
            {
                throw new System.NotImplementedException();
            }

            public void Save(User item)
            {
                throw new System.NotImplementedException();
            }
        }
    }
} 
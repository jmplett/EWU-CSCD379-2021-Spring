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

        [TestMethod]
        public void Delete_WithId_ReturnsOk() {
            //Arrange
            UserRepository repository = new();
            UsersController controller = new(repository);
            
            //Act
            ActionResult result = controller.Delete(1);

            //Assert
            Assert.IsTrue(result is OkResult);
        }

        [TestMethod]
        [DataRow(-123)]
        [DataRow(-89)]
        public void Delete_WithNegativeId_ReturnsNotFound(int index) {
            //Arrange
            TestableUserRepository repository = new();
            UsersController controller = new(repository);

            //Act
            ActionResult result = controller.Delete(index);

            //Assert
            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        [DataRow (22)]
        public void Post_WithUserData_ReturnsUserData(int Index) {
            //Arrange
            UserRepository repository = new();
            UsersController controller = new(repository);
            User newUser = new User() {Id = Index, FirstName = "Bob", LastName = "Sam"};

            //Act
            User createdUser = controller.Post(newUser).Value;

            //Assert
            Assert.AreEqual(repository.GetItem(Index).Id, newUser.Id);
        }

        [TestMethod]
        public void Post_WithNullData_ReturnsBadRequest() {
            //Arrange
            UserRepository repository = new();
            UsersController controller = new(repository);
            User myUser = null;

            //Act
            ActionResult<User?> result = controller.Post(myUser);

            //Assert
            Assert.IsTrue(result.Result is BadRequestResult);
        }

        [TestMethod]
        public void Put_WithIdAndData_ReturnsOk() {
            //Arrange
            UserRepository repository = new();
            UsersController controller = new(repository);
            User newUser = new User() {Id = 43, FirstName = "Bob", LastName = "Sam"};

            //Act
            ActionResult result = controller.Put(5, newUser);

            //Assert
            Assert.IsTrue(result is OkResult);
        }

        [TestMethod]
        public void Put_WithIdAndNullData_ReturnsBadRequest() {
            //Arrange
            UserRepository repository = new();
            UsersController controller = new(repository);
            User newUser = null;

            //Act
            ActionResult result = controller.Put(10, newUser);

            //Assert
            Assert.IsTrue(result is BadRequestResult);
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
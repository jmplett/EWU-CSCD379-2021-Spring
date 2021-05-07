using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.Tests.Business;
using SecretSanta.Api.Dto;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Constructor_WithNullRepository_ThrowException()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => new UsersController(null!));
            Assert.AreEqual("repository", ex.ParamName);
        }

        [TestMethod]
        public async Task GetAll_WithValidUsers_ReturnsUsers()
        {
            //Arrange
            using WebApplicationFactory factory = new();
            TestableUserRepository repository = factory.UserRepository;
            User user1 = new User()
            {
                Id = 122,
                FirstName = "Tim",
                LastName = "Timmy"
            };

             User user2 = new User()
            {
                Id = 123,
                FirstName = "Jimmy",
                LastName = "John"
            };

            repository.GetUserList= new List<User>() { user1, user2};

            HttpClient client = factory.CreateClient();
            
            //Act
            List<FullUser>? users = await client.GetFromJsonAsync<List<FullUser>>("/api/users");

            //Assert
            Assert.AreEqual(122, users[0].Id);
            Assert.AreEqual("Tim", users[0].FirstName);
            Assert.AreEqual("Timmy", users[0].LastName);
        }

        [TestMethod]
        public async Task GetWithId_WithUser_ReturnsUser()
        {
            //Arrange
            using WebApplicationFactory factory = new();
            TestableUserRepository repository = factory.UserRepository;
            User user = new User() 
            { 
                Id = 122, 
                FirstName = "Tim", 
                LastName = "Timmy" 
            };
            repository.GetItemUser = user;

            HttpClient client = factory.CreateClient();

            //Act
            FullUser? retrievedUser = await client.GetFromJsonAsync<FullUser>("/api/users/122");

            //Assert
            Assert.AreEqual(122, retrievedUser.Id);
            Assert.AreEqual("Tim", retrievedUser.FirstName);
            Assert.AreEqual("Timmy", retrievedUser.LastName);
        }

        [TestMethod]
        public async Task Get_WithInvalidId_ReturnsNotFound()
        {
            //Arrange
            using WebApplicationFactory factory = new();
            TestableUserRepository repository = factory.UserRepository;
            User user = new()
            {
                Id = 122,
                FirstName = "Tim",
                LastName = "Timmy"
            };
            repository.Create(user);

            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.GetAsync("/api/users/41");

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task Delete_WithValidId_RemovesItem()
        {
            //Arrange
            using WebApplicationFactory factory = new();
            TestableUserRepository repository = factory.UserRepository;
            User user = new()
            {
                Id = 122,
                FirstName = "Tim",
                LastName = "Timmy"
            };
            repository.UserToRemove = user;

            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.DeleteAsync("/api/users/122");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(0, repository.List().Count);
        }

        [TestMethod]
        public async Task Delete_WithInvalidId_ReturnsNotFound()
        {
            //Arrange
            using WebApplicationFactory factory = new();
            TestableUserRepository repository = factory.UserRepository;
            User user = new()
            {
                Id = 122,
                FirstName = "Tim",
                LastName = "Timmy"
            };
            repository.UserToRemove = user;

            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.DeleteAsync("/api/users/41");

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Post_WithValidData_ReturnsCreatesUser()
        {
            //Arrange
            using WebApplicationFactory factory = new();
            TestableUserRepository repository = factory.UserRepository;
            User user = new()
            {
                Id = 122,
                FirstName = "Tim",
                LastName = "Timmy"
            };
            repository.MyUser = user;

            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.PostAsJsonAsync("/api/users", user);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Update_WithValidData_ReturnsUpdatesUser()
        {
            //Arrange
            using WebApplicationFactory factory = new();
            TestableUserRepository repository = factory.UserRepository;
            User user = new()
            {
                Id = 122,
                FirstName = "Tim",
                LastName = "Timmy"
            };
            repository.GetItemUser = user;
            HttpClient client = factory.CreateClient();

            //Act
            User updatedUser = new User       
            {
                FirstName = "Bob",
                LastName = "Builder"
            };
            HttpResponseMessage response = await client.PutAsJsonAsync("/api/users/122", updatedUser);
            

            //Assert
            response.EnsureSuccessStatusCode();
            var createdUser = repository.GetItem(122);
            Assert.AreEqual(122, createdUser.Id);
            Assert.AreEqual("Bob", createdUser.FirstName);
            Assert.AreEqual("Builder", createdUser.LastName);
        }

        [TestMethod]
        public async Task Update_InvalidUserId_ReturnsNotFound()
        {
            //Arrange
           using WebApplicationFactory factory = new();
            TestableUserRepository repository = factory.UserRepository;
            User user = new()
            {
                Id = 122,
                FirstName = "Tim",
                LastName = "Timmy"
            };
            repository.GetItemUser = user;
            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.PutAsJsonAsync("/api/users/987", new UpdateUser       
            {
                FirstName = "Bob",
                LastName = "Builder"
            });

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            var createdUser = repository.GetItem(122);
            Assert.AreEqual(122, createdUser.Id);
            Assert.AreEqual("Tim", createdUser.FirstName);
            Assert.AreEqual("Timmy", createdUser.LastName);
        }
    }
}

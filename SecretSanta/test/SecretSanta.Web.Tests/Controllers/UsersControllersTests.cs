 using System;
 using System.Collections.Generic;
 using System.Net.Http;
 using System.Threading.Tasks;
 using Microsoft.VisualStudio.TestTools.UnitTesting;
 using SecretSanta.Web.Controllers;
 using SecretSanta.Web.Tests.Api;
 using SecretSanta.Web.ViewModels;
 using SecretSanta.Web.Api;

namespace SecretSanta.Web.Tests.Controllers
{
    [TestClass]
    public class UsersControllersTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullRepository_ThrowException()
        {
           Web.Controllers.UsersController controller = new(null!);
        }

        [TestMethod]
        public async Task Index_WithUsers_ReturnsUsers()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUsersClient usersClient = factory.Client;

            await usersClient.PostAsync(new FullUser
            {
                Id = 123,
                FirstName = "Bob",
                LastName = "Builder"
            });

            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.GetAsync("/users/index");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(1, usersClient.GetAllAsyncInvocationCount);
        }

        [TestMethod]
        public async Task Create_WithValidModel_CreatesUser()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUsersClient usersClient = factory.Client;

            HttpClient client = factory.CreateClient();

            var values = new Dictionary<string?, string?>
            {
                { nameof(UserViewModel.Id), "123" },
                { nameof(UserViewModel.FirstName), "Bob" },
                { nameof(UserViewModel.LastName), "Builder" },
            };
            FormUrlEncodedContent content = new(values!);

            //Act
            HttpResponseMessage response = await client.PostAsync("/users/create", content);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(1, usersClient.PostAsyncInvokedParameters.Count);
        }

        [TestMethod]
        public async Task Create_WithInvalidModel_CreatesUser()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUsersClient usersClient = factory.Client;
            int startingInvocation = usersClient.PostAsyncInvocationCount;

            HttpClient client = factory.CreateClient();

            var values = new Dictionary<string?, string?>
            {
                { nameof(UserViewModel.Id), "123" },
                { nameof(UserViewModel.FirstName), "" }
            };
            FormUrlEncodedContent content = new(values!);

            //Act
            HttpResponseMessage response = await client.PostAsync("/users/create", content);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(startingInvocation + 1, usersClient.PostAsyncInvocationCount);
        }

        [TestMethod]
        public async Task Edit_WithUserId_InvokesAsync()
        {
            //Arrange
            WebApplicationFactory factory = new();
            FullUser user = new() { Id = 909, FirstName = "Place0", LastName = "Holder0" };
            TestableUsersClient usersClient = factory.Client;
            usersClient.GetAsyncFullUser = user;
            int startingInvocation = usersClient.GetAsyncInvocationCount;

            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.GetAsync("/Users/Edit/" + user.Id);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(startingInvocation + 1, usersClient.GetAsyncInvocationCount);
        }

        [TestMethod]
        public async Task Edit_WithValidModel_UpdatesUser()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUsersClient usersClient = factory.Client;
            await usersClient.PostAsync(new FullUser
            {
                Id = 123,
                FirstName = "Bob",
                LastName = "Builder"
            });
            HttpClient client = factory.CreateClient();

            var values = new Dictionary<string?, string?>
            {
                { nameof(UserViewModel.FirstName), "Timmy" },
                { nameof(UserViewModel.LastName), "Tim" },
            };
            FormUrlEncodedContent content = new(values);

            //Act
            HttpResponseMessage response = await client.PostAsync("/users/edit", content);

            //Assert
            //response.EnsureSuccessStatusCode();
            Assert.AreEqual(1, usersClient.PutAsyncInvocationCount);
        }

        [TestMethod]
        public async Task Edit_WithInvalidModel_WillNotUpdateUser()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUsersClient usersClient = factory.Client;
             int startingInvocation = usersClient.PutAsyncInvocationCount;
            await usersClient.PostAsync(new FullUser
            {
                Id = 123,
                FirstName = "Bob",
                LastName = "Builder"
            });
            HttpClient client = factory.CreateClient();

            var values = new Dictionary<string?, string?>
            {
                { nameof(UserViewModel.Id), "123" },
                { nameof(UserViewModel.FirstName), "" }
            };
            FormUrlEncodedContent content = new(values);

            //Act
            HttpResponseMessage response = await client.PostAsync("/users/edit", content);

            //Assert
            //response.EnsureSuccessStatusCode();
            Assert.AreEqual(startingInvocation + 1, usersClient.PutAsyncInvocationCount);
        }

        [TestMethod]
        public async Task Delete_WithUserId_RemovesUsers()
        {
            //Arrange 
            WebApplicationFactory factory = new();
            TestableUsersClient usersClient = factory.Client;
            await usersClient.PostAsync(new FullUser
            {
                Id = 123,
                FirstName = "Bob",
                LastName = "Builder"
            });
            HttpClient client = factory.CreateClient();

            var values = new Dictionary<string?, string?>
            {
                { nameof(UserViewModel.Id), "123" }
            };
             FormUrlEncodedContent content = new(values);

            //Act
            HttpResponseMessage response = await client.PostAsync("/users/delete", content);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(1, usersClient.DeleteInvocationCount);
            Assert.IsNull(await usersClient.GetAsync(123));
        }
    }
}

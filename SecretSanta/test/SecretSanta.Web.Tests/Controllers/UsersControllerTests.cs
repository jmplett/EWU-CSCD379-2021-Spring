using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SecretSanta.Web.Api;
using SecretSanta.Web.ViewModels;
using System.Collections.Generic;
using System;
using SecretSanta.Web.Tests.Api;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class UsersControllersTests
    {
        private WebApplicationFactory Factory { get; } = new();

        [TestMethod]
        public async Task Index_WithEvents_InvokesGetAllSync()
        {
            //Arrange
            User user1 = new()
            {
                Id = 1,
                FirstName = "bob",
                LastName = "bobby"
            };
            User user2 = new()
            {
                Id = 2,
                FirstName = "Joe",
                LastName = "Joey"
            };
            TestableUsersClient usersClient = Factory.Client;

            HttpClient client = Factory.CreateClient();

            //Act
            HttpResponse responce = await client.getAsync("/Users");

            //Assert
            responce.EnsureSuccessStatusCode();
        }
    }
}

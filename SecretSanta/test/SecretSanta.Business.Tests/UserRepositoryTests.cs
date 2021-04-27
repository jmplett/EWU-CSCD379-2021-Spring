using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using SecretSanta.Business;
using SecretSanta.Data; 

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {

        [TestMethod]
        [DataRow(172)]
        [DataRow(13)]
        public void Create_WithUserData_ReturnsUserData(int index) 
        {
            UserRepository repository = new();

            User newUser = new User() {Id = index, FirstName = "Luther", LastName = "Hanson"};
            repository.Create(newUser);

            Assert.IsTrue(repository.GetItem(index).Id == newUser.Id);
            Assert.IsTrue(repository.GetItem(index).FirstName == newUser.FirstName);
            Assert.IsTrue(repository.GetItem(index).LastName == newUser.LastName);
        }

        [TestMethod]
        public void GetItem_ValidId_ReturnsUserOfSameId()
        {
            UserRepository repository = new();

            User expectedUser = new User() {Id = 192, FirstName = "Luther", LastName = "Hanson"};
            repository.Create(expectedUser);

            Assert.AreEqual(repository.GetItem(192).Id, expectedUser.Id);
        }

        [TestMethod]
        public void List_WithData_ReturnsUsersInList()
        {
            UserRepository repository = new();

            ICollection<User> expectedData = DeleteMe.Users;
            ICollection<User> testData = repository.List();

            Assert.AreEqual(expectedData, testData);
        }

        [TestMethod]
        [DataRow(100)]
        [DataRow(23)]
        
        public void Remove_WithId_RemovesUserAtId(int index)
        {
            UserRepository repository = new();
            User expectedUser = new User() {Id = index, FirstName = "Luther", LastName = "Hanson"};
            repository.Create(expectedUser);

            Assert.IsNotNull(repository.GetItem(index));
            Assert.IsTrue(repository.Remove(index));
             Assert.IsNull(repository.GetItem(index));
        }

        [TestMethod]
        [DataRow(-100)]
        [DataRow(-23)]
        public void Remove_WithNegitiveId_ReturnsFalse(int index)
        {
            UserRepository repository = new();

            Assert.IsFalse(repository.Remove(index));
        }

        [TestMethod]
        [DataRow(100)]
        [DataRow(23)]
        public void Save_WithExistingData_UpdatesUserInfo(int index)
        {
            UserRepository repository = new();
            User unmodifiedUser = new User() {Id = index, FirstName = "Luther", LastName = "Hanson"};
            repository.Create(unmodifiedUser);

            User modifiedUser = new User() {Id = index, FirstName = "Modified", LastName = "User"};
            repository.Save(modifiedUser);

            Assert.AreNotEqual(repository.GetItem(index).FirstName, unmodifiedUser.FirstName);
            Assert.AreNotEqual(repository.GetItem(index).LastName, unmodifiedUser.LastName);
            Assert.AreEqual(repository.GetItem(index).FirstName, modifiedUser.FirstName);
            Assert.AreEqual(repository.GetItem(index).LastName, modifiedUser.LastName);
        }
    }
}

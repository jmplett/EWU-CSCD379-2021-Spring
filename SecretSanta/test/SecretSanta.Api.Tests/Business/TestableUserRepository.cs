namespace SecretSanta.Api.Tests.Business
{
    using System.Collections.Generic;
    using SecretSanta.Business;
    using SecretSanta.Data;

    public class TestableUserRepository : IUserRepository
    {
        public User? MyUser { get; set; } = null;

         public User Create(User item)
        {
            MyUser = item;
            return MyUser;
        }

        public User? GetItemUser { get; set; }
        public int GetItemId { get; set; }
        public User? GetItem(int id)
        {
            GetItemId = id;
            return GetItemUser;
        }

        public List<User>? GetUserList { get; set; } = new();
        public ICollection<User> List()
        {
            return GetUserList;
        }

        public User? UserToRemove { get; set; } = new();
        public bool DeleteResult { get; set; } = false;
        public bool Remove(int id)
        {
            User? userBefore = UserToRemove;

            if(userBefore is null || UserToRemove is null || id != UserToRemove.Id)
            {
                return false;
            }

            UserToRemove = null;

            DeleteResult = ! userBefore.Equals(UserToRemove);
            return DeleteResult;
        }

        public User? SavedUser {get; set;}
        public void Save(User item)
        {
            SavedUser = item;
        }

    }
}
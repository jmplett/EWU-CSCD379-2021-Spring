using System;
using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class GroupRepository : IGroupRepository
    {
        public Group Create(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            MockData.Groups[item.Id] = item;
            return item;
        }

        public Group? GetItem(int id)
        {
            if (MockData.Groups.TryGetValue(id, out Group? user))
            {
                return user;
            }
            return null;
        }

        public ICollection<Group> List()
        {
            return MockData.Groups.Values;
        }

        public bool Remove(int id)
        {
            return MockData.Groups.Remove(id);
        }

        public void Save(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            MockData.Groups[item.Id] = item;
        }


        public AssignmentResult Assign(int id)
        {
            if (!MockData.Groups.ContainsKey(id)) {
                return AssignmentResult.Error("Group not found");
            }

            MockData.Groups[id].Assignments = new();

            List<User> users = MockData.Groups[id].Users;

            if (users.Count < 3) {
                return AssignmentResult.Error("Group must have at least three users");
            }

            Shuffle(users);

            for (int i = 0; i < users.Count; i++) {
                if (i < users.Count - 1) {
                    MockData.Groups[id].Assignments.Add(new Assignment(users[i], users[i + 1]));
                } else {
                    MockData.Groups[id].Assignments.Add(new Assignment(users[i], users[0]));
                }
            }

            return AssignmentResult.Success();
        }

        private void Shuffle<T>(IList<T> list)  
        {  
            Random rng = new Random();
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = rng.Next(n + 1);
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }
    }
}

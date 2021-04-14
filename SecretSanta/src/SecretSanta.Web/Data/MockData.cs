using System.Collections.Generic;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Data
{
    public static class MockData
    {
        public static List<UserViewModel> Users = new List<UserViewModel>
        {
            new UserViewModel { Id = 1, FirstName = "Justin", LastName = "Plett" },
            new UserViewModel { Id = 2, FirstName = "John", LastName = "David" },
            new UserViewModel { Id = 3, FirstName = "Tommy", LastName = "Tall" },
            new UserViewModel { Id = 4, FirstName = "Jimmy", LastName = "Little" },
            new UserViewModel { Id = 5, FirstName = "Stuart", LastName = "Little" },
        };

        public static List<GroupViewModel> Groups = new List<GroupViewModel>
        {
            new GroupViewModel { Id = 1, GroupName = "The Cool Peeps" },
            new GroupViewModel { Id = 2, GroupName = "WE'RE TOO COOL FOR A NAME" },
        };

        public static List<GiftViewModel> Gifts = new List<GiftViewModel>
        {
            new GiftViewModel { Id = 1, GiftName = "Bouncy Ball", GiftDescription = "This is a ball that bounces", GiftURL="https://Amazon.com", GiftPriority = 3, GiftUser = 1 },
            new GiftViewModel { Id = 1, GiftName = "Gold Fish", GiftDescription = "This is a fish that is gold", GiftURL="https://Amazon.com", GiftPriority = 3, GiftUser = 2 },
            new GiftViewModel { Id = 1, GiftName = "Baseball Cards", GiftDescription = "A card with a random dudes face on it", GiftURL="https://Amazon.com", GiftPriority = 1, GiftUser = 4 },
            new GiftViewModel { Id = 1, GiftName = "One Million Dollars", GiftDescription = "You will be forever my friend", GiftURL="https://Amazon.com", GiftPriority = 2, GiftUser = 3 },
            new GiftViewModel { Id = 1, GiftName = "Cassette Player", GiftDescription = "An old music holder thingy", GiftURL="https://Amazon.com", GiftPriority = 1, GiftUser = 3 },
            new GiftViewModel { Id = 1, GiftName = "Playing Cards", GiftDescription = "So I can play 52 card pick up", GiftURL="https://Amazon.com", GiftPriority = 1, GiftUser = 2 },
            new GiftViewModel { Id = 1, GiftName = "Fireworks", GiftDescription = "They Fly and go BOOOOOM!", GiftURL="https://Amazon.com", GiftPriority = 1, GiftUser = 1 },
            new GiftViewModel { Id = 1, GiftName = "Toy Car", GiftDescription = "I can make cool vrooom vrooom sounds!", GiftURL="https://Amazon.com", GiftPriority = 2, GiftUser = 1 },
        };
    }
}
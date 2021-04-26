using System.Collections.Generic;

namespace SecretSanta.Data
{
    public static class DeleteMe
    {
        public static List<User> Users { get; } = new()
        {
            new User() { Id=0, FirstName="Justin", LastName="Plett" },
            new User() { Id=1, FirstName="Tim", LastName="Tracker" },
            new User() { Id=2, FirstName="Poly", LastName="Paul" },
            new User() { Id=3, FirstName="Percy", LastName="Briggs" },
            new User() { Id=4, FirstName="Sara", LastName="Barnes" },
            new User() { Id=5, FirstName="Lillian", LastName="Munoz" }
        };
    }
}
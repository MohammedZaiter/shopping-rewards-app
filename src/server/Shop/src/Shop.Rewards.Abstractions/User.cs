using System.Collections.Generic;

namespace Shop.Rewards.Abstractions
{
    internal class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public List<Purchase> Purchases { get; set; }
    }
}

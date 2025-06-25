using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Rewards.Abstractions
{
    internal class Purchase
    {
        public int Id { get; set; }

        public string StoreName { get; set; }

        public string Category { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int Points => (int)(Amount / 10); // 1 point per $10

        // Foreign Key
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

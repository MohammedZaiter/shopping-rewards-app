﻿
namespace Shop.Rewards.Store
{
    using Shop.Rewards.Models;
    using System;

    public class PurchaseRecord
    {
        public PurchaseRecord(Guid id, Guid userId, string storeName, Category category, decimal amount, DateTime createdAt)
        {
            this.Id = id;
            this.UserId = userId;
            this.StoreName = storeName ?? throw new ArgumentNullException(nameof(id));
            this.Category = category;
            this.Amount = amount;
            this.CreatedAt = createdAt;
        }

        public Guid Id
        {
            get;
            set;
        }

        public Guid UserId
        {
            get;
            set;
        }

        public string StoreName
        {
            get;
            set;
        }

        public Category Category
        {
            get;
            set;
        }

        public decimal Amount
        {
            get;
            set;
        }

        public DateTime CreatedAt
        {
            get;
            set;
        }

        public static PurchaseRecord ConvertToPurchaseRecord(PurchaseRequest request)
        {
            return new PurchaseRecord(Guid.NewGuid(),
                request.UserId, request.StoreName,
                request.Category, request.Amount, DateTime.UtcNow);
        }

        public static Purchase ConvertToPurchaseModel(PurchaseRecord record)
        {
            return new Purchase(record.Id,
                record.UserId, record.StoreName,
                record.Category, record.Amount, record.CreatedAt);
        }
    }
}

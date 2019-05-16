using System;
using Mentoring.Lab7.Data.Entities;
using MongoDB.Driver;

namespace Mentoring.Lab7.Data
{
    public class MongoDataContext : IMongoContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDataContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"{nameof(connectionString)} must be set.");
            }

            var databaseName = MongoUrl.Create(connectionString)?.DatabaseName;

            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException($"database name not found to {nameof(connectionString)}.");
            }

            var server = new MongoClient(connectionString);
            _mongoDatabase = server.GetDatabase(databaseName);
        }

        public IMongoCollection<Book> Books => _mongoDatabase.GetCollection<Book>("books");
    }
}

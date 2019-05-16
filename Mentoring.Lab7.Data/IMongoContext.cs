using Mentoring.Lab7.Data.Entities;
using MongoDB.Driver;

namespace Mentoring.Lab7.Data
{
    public interface IMongoContext
    {
        IMongoCollection<Book> Books { get; }
    }
}

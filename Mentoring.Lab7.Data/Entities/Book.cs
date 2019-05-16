using System.Collections.Generic;
using MongoDB.Bson;

namespace Mentoring.Lab7.Data.Entities
{
    public class Book
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Count { get; set; }
        public List<string> Genres { get; set; }
        public int Year { get; set; }
    }
}

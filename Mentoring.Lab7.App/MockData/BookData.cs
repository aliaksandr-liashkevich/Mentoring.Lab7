using System.Collections.Generic;
using Mentoring.Lab7.Data.Entities;

namespace Mentoring.Lab7.App.MockData
{
    public static class BookData
    {
        public static IEnumerable<Book> Books =>
            new Book[]
            {
                new Book
                {
                    Name = "Hobbit",
                    Author = "Tolkien",
                    Count = 5,
                    Genres = new List<string>
                    {
                        "fantasy"
                    },
                    Year = 2014
                },
                new Book
                {
                    Name = "Lord of the rings",
                    Author = "Tolkien",
                    Count = 3,
                    Genres = new List<string>
                    {
                        "fantasy"
                    },
                    Year = 2015
                },
                new Book
                {
                    Name = "Kolobok",
                    Count = 10,
                    Genres = new List<string>
                    {
                        "kids"
                    },
                    Year = 2000
                },
                new Book
                {
                    Name = "Repka",
                    Count = 11,
                    Genres = new List<string>
                    {
                        "kids"
                    },
                    Year = 2000
                },
                new Book
                {
                    Name = "Dyadya Stiopa",
                    Author = "Mihalkov",
                    Count = 1,
                    Genres = new List<string>
                    {
                        "kids"
                    },
                    Year = 2001
                }
            };
    }
}

using System;
using System.Threading.Tasks;
using Mentoring.Lab7.App.MockData;
using Mentoring.Lab7.Data.Entities;
using MongoDB.Driver;

namespace Mentoring.Lab7.App
{
    public class App
    {
        private readonly IMongoCollection<Book> _bookCollection;

        public App(IMongoCollection<Book> bookCollection)
        {
            _bookCollection = bookCollection;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Task 1.");
            await AddBooksIfNotExistsAsync();
            var books = await _bookCollection.Find(_ => true).ToListAsync();
            PrintBooks(books.ToArray());

            Console.WriteLine("Task 2.");
            await PrintBookNamesByCountAsync();

            Console.WriteLine("Task 3.");
            await PrintBookWithMinAndMaxCountAsync();

            Console.WriteLine("Task 4.");
            await PrintUniqueAuthorsAsync();

            Console.WriteLine("Task 5.");
            await PrintBooksWithoutAuthorsAsync();

            Console.WriteLine("Task 6.");
            await IncreaseAllCountAndPrintAsync();

            Console.WriteLine("Task 7.");
            await AddNewGenreAndPrintAsync();

            Console.WriteLine("Repeat Task 7.");
            await AddNewGenreAndPrintAsync();

            Console.WriteLine("Task 8.");
            await RemovByCountAndPrintAsync();

            Console.WriteLine("Task 9.");
            await RemoveAllAndPrintAsync();
        }

        private async Task AddBooksIfNotExistsAsync()
        {
            var count = await _bookCollection.CountDocumentsAsync(_ => true);
            if (count == 0)
            {
                _bookCollection.InsertMany(BookData.Books);
            }
        }

        private void PrintBooks(params Book[] books)
        {
            Console.WriteLine(new string('-', 50));

            if (books != null && books.Length != 0)
            {
                Console.WriteLine($"Books count: {books.Length}");

                foreach (var book in books)
                {
                    Console.WriteLine($"Book name: {book.Name}, count = {book.Count}, author = {book.Author ?? "-"}");

                    foreach (var bookGenre in book.Genres)
                    {
                        Console.WriteLine($"- book genre: {bookGenre}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No books.");
            }

            Console.WriteLine(new string('-', 50));
        }

        private async Task PrintBookNamesByCountAsync(int count = 1)
        {
            Console.WriteLine(new string('-', 50));

            var bookNames = await _bookCollection.Find(book => book.Count > count)
                .SortBy(x => x.Name)
                .Limit(3)
                .Project(x => x.Name)
                .ToListAsync();

            Console.WriteLine($"Book names count: {bookNames.Count}");

            foreach (var bookName in bookNames)
            {
                Console.WriteLine(bookName);
            }

            Console.WriteLine(new string('-', 50));
        }

        private async Task PrintBookWithMinAndMaxCountAsync()
        {
            var minCountBook = await _bookCollection.Find(_ => true)
                .SortBy(b => b.Count)
                .Limit(1)
                .FirstOrDefaultAsync();

            Console.WriteLine("Min count book.");
            PrintBooks(minCountBook);

            var maxCountBook = await _bookCollection.Find(_ => true)
                .SortByDescending(b => b.Count)
                .Limit(1)
                .FirstOrDefaultAsync();

            Console.WriteLine("Max count book.");
            PrintBooks(maxCountBook);
        }

        private async Task PrintUniqueAuthorsAsync()
        {
            var uniqueAuthors = await _bookCollection.DistinctAsync(x => x.Author, _ => true);

            Console.WriteLine(new string('-', 50));

            foreach (var uniqueAuthor in uniqueAuthors.ToEnumerable())
            {
                Console.WriteLine($"Author: {uniqueAuthor ?? "-"}");
            }

            Console.WriteLine(new string('-', 50));
        }

        private async Task PrintBooksWithoutAuthorsAsync()
        {
            var booksWithoutAuthors = await _bookCollection.Find(b => string.IsNullOrEmpty(b.Author)).ToListAsync();

            PrintBooks(booksWithoutAuthors.ToArray());
        }

        private async Task IncreaseAllCountAndPrintAsync()
        {
            var update = Builders<Book>.Update.Inc(x => x.Count, 1);

            _bookCollection.UpdateMany(_ => true, update);

            var books = await _bookCollection.Find(_ => true).ToListAsync();
            PrintBooks(books.ToArray());
        }

        private async Task AddNewGenreAndPrintAsync()
        {
            const string newGenre = "favority";

            var filter = Builders<Book>.Filter.AnyNin(b => b.Genres, new[] {newGenre});
            var update = Builders<Book>.Update.AddToSet(x => x.Genres, newGenre);

            _bookCollection.UpdateMany(filter, update);

            var books = await _bookCollection.Find(_ => true).ToListAsync();
            PrintBooks(books.ToArray());
        }

        private async Task RemovByCountAndPrintAsync()
        {
            var count = 3;
            await _bookCollection.DeleteManyAsync(b => b.Count < count);

            var books = await _bookCollection.Find(_ => true).ToListAsync();
            PrintBooks(books.ToArray());
        }

        private async Task RemoveAllAndPrintAsync()
        {
            await _bookCollection.DeleteManyAsync(_ => true);

            var books = await _bookCollection.Find(_ => true).ToListAsync();
            PrintBooks(books.ToArray());
        }
    }
}

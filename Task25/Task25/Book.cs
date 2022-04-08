using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Task25
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Genre { get; set; }

        public string Authors { get; set; }

        /// <summary>
        /// Пользователь, у которого на руках данная книга в текущий момент времени
        /// </summary>
        public User user { get; set; }
    }

    public class BookRepository
    {
        AppContext db;
        DbSet<Book> books;

        public BookRepository(AppContext context)
        {
            db = context;
            books = db.Books;
        }

        public Book GetBookById(int id) => books.FirstOrDefault(b => b.Id == id);

        public IEnumerable<Book> GetAllBooks() => books;

        public void AddBook(Book book) => books.Add(book);

        public void AddSeveralBook(Book[] booksArray) => books.AddRange(booksArray);

        public void RemoveBook(Book book) => books.Remove(book);

        public IEnumerable<Book> GetBooksByGenre(string genre) => books.Where(b => b.Genre == genre).ToList();

        public IEnumerable<Book> GetBooksByDate(DateTime start, DateTime end) => books.Where(b => b.Date > start && b.Date < end);

        public int GetBookCountByAuthor(string author) => books.Count(b => b.Authors == author);

        public int GetBookCountByGenre(string genre) => books.Count(b => b.Genre == genre);

        public bool BookExistByAuthorAndName(string author, string name) => books.Any(b => b.Authors == author && b.Name == name);

        public bool UserHasBook(Book book) => books.Any(b => b.user != null);

        public Book GetLastBook() => books.FirstOrDefault(b => b.Date == books.Max(item => item.Date));
    }
}

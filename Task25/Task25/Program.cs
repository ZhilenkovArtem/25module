using System;
using System.Collections.Generic;
using System.Linq;

namespace Task25
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppContext())
            {
                var user1 = new User { Name = "Артем", Email = "mail@mail.ru" };
                var user2 = new User { Name = "Достоевский", Email = "19century@mail.ru" };
                var userRepository = new UserRepository(db);
                userRepository.AddSeveralUser(new User[] { user1, user2 });
                db.SaveChanges();

                var book1 = new Book { Name = "Ленин и Комсомольский камень", Genre = "фантастика", Date = new DateTime(2020, 02, 07), Authors = "Джон Ролик" };
                var book2 = new Book { Name = "Ленин и Красная комната", Genre = "ужасы", Date = new DateTime(2021, 03, 08), Authors = "Джоан Роулинг" };
                var book3 = new Book { Name = "Ленин и Кубок вечного огня", Genre = "ужасы", Date = new DateTime(2022, 04, 09), Authors = "Джон Ролик" };
                var bookRepository = new BookRepository(db);
                bookRepository.AddSeveralBook(new Book[] { book1, book2, book3});
                db.SaveChanges();

                user1.Books.AddRange(new List<Book>() { book2, book3 });
                user2.Books.AddRange(new List<Book>() { book1, book2 });
                db.SaveChanges();

                // 1
                Console.WriteLine("Введите жанр, книги с которым вы хотите получить:");
                var bookByGenre = bookRepository.GetBooksByGenre(Console.ReadLine());
                foreach (var book in bookByGenre)
                    Console.WriteLine(book.Name);

                Console.WriteLine("\nВведите год, с которого начинать искать книгу:");
                int initialYear, endingYear;
                var initialYearParsingTrue = Int32.TryParse(Console.ReadLine(), out initialYear);
                Console.WriteLine("Введите год, которым закончить искать книгу:");
                var endingYearParsingTrue = Int32.TryParse(Console.ReadLine(), out endingYear);
                if (initialYearParsingTrue && endingYearParsingTrue)
                {
                    var booksByDate = bookRepository.GetBooksByDate(
                                        new DateTime(initialYear, 01, 01),
                                        new DateTime(endingYear, 12, 31));
                    foreach (var book in booksByDate)
                        Console.WriteLine(book.Name);
                }
                // 2
                Console.WriteLine("\nВведите автора, число книг которого вы хотите получить:");
                Console.WriteLine(bookRepository.GetBookCountByAuthor(Console.ReadLine()));
                // 3
                Console.WriteLine("\nВведите жанр, число книг которого вы хотите получить:");
                Console.WriteLine(bookRepository.GetBookCountByGenre(Console.ReadLine()));
                // 4
                Console.WriteLine("\nВведите автора:");
                var author = Console.ReadLine();
                Console.WriteLine("Введите название книги:");
                var name = Console.ReadLine();
                Console.WriteLine(bookRepository.BookExistByAuthorAndName(author, name));
                // 5
                bool userHasBook = bookRepository.UserHasBook(book1);
                string str = userHasBook ? "присутствует" : "отсутствует";
                Console.WriteLine($"\nКнига \"{book1.Name}\" у пользователя {str}");
                // 6
                Console.WriteLine($"\nПользователь {user1.Name} на руках имеет книг: {userRepository.UserHasBookCount(user1)}.");
                // 7
                Console.WriteLine($"\nПоследняя вышедшая книга: \"{bookRepository.GetLastBook().Name}\"");
                // 8
                Console.WriteLine("\nКниги, отсортированные в алфавитном порядке по названию:");
                foreach (var book in db.Books.OrderBy(b => b.Name))
                    Console.WriteLine(book.Name);
                // 9
                Console.WriteLine("\nКниги, отсортированные в порядке убывания года их выхода:");
                foreach (var book in db.Books.OrderByDescending(b => b.Date))
                    Console.WriteLine($"{book.Date.ToShortDateString()}\t{book.Name}");
            }
            Console.ReadKey();
        }
    }
}

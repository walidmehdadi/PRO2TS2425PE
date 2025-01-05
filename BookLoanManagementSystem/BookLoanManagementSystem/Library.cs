using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoanManagementSystem
{
    public class Library
    {
        private readonly IBookInfoProvider bookInfoProvider;
        private readonly IBookManagement bookManagement;

        public Library(IBookInfoProvider bookInfoProvider, IBookManagement bookManagement)
        {
            this.bookInfoProvider = bookInfoProvider;
            this.bookManagement = bookManagement;
        }

        public void Work()
        {
            Console.WriteLine("Library system is running...");
            Console.WriteLine("Available options:");
            Console.WriteLine("1. Borrow a book");
            Console.WriteLine("2. Return a book");
            Console.WriteLine("3. View available books");
            Console.WriteLine("4. Exit");

            while (true)
            {
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BorrowBookInteraction();
                        break;
                    case "2":
                        ReturnBookInteraction();
                        break;
                    case "3":
                        DisplayAvailableBooks();
                        break;
                    case "4":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void BorrowBookInteraction()
        {
            try
            {
                Console.Write("Enter the book ID: ");
                int bookId = int.Parse(Console.ReadLine());
                Console.Write("Enter the user ID: ");
                int userId = int.Parse(Console.ReadLine());

                // Borrow the book using BookManagement
                bookManagement.BorrowBook(bookId, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void ReturnBookInteraction()
        {
            try
            {
                Console.Write("Enter the book ID: ");
                int bookId = int.Parse(Console.ReadLine());

                bookManagement.ReturnBook(bookId);
                Console.WriteLine("Book successfully returned.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void DisplayAvailableBooks()
        {
            List<Book> availableBooks = new List<Book>();

            // Use GetBooks method to get all books and check availability
            var allBooks = bookInfoProvider.GetBooks();
            foreach (var book in allBooks)
            {
                if (book.IsAvailable()) // Check if the book is available
                {
                    availableBooks.Add(book);
                }
            }

            if (availableBooks.Count == 0)
            {
                Console.WriteLine("No books are currently available.");
            }
            else
            {
                Console.WriteLine("Available books:");
                foreach (var book in availableBooks)
                {
                    Console.WriteLine($"ID: {book.Id}, Title: {book.Title}");
                }
            }
        }

    }
}

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

        public void BorrowBookInteraction(int bookId, int userId, string returnDateInput)
        {
            try
            {
                if (!IsValidReturnDate(returnDateInput))
                {
                    Console.WriteLine("Error: Invalid return date. Return date cannot be in the past.");
                    return;
                }

                // Fetch book info and handle any potential exception
                var book = bookInfoProvider.GetBookInfo(bookId);
                if (book == null || !book.isAvailable)
                {
                    Console.WriteLine("Error: Book not available.");
                    return;
                }

                // Proceed with borrowing the book
                bookManagement.BorrowBook(bookId, userId);
                Console.WriteLine("Book borrowed");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Unable to fetch book information.");
                // Handle the exception and prevent any further action
            }
        }

        public void BorrowBooksInteraction(List<int> bookIds, int userId, string returnDateInput)
        {
            foreach (var bookId in bookIds)
            {
                try
                {
                    if (!IsValidReturnDate(returnDateInput))
                    {
                        Console.WriteLine("Error: Invalid return date. Return date cannot be in the past.");
                        return;
                    }

                    // Fetch book info and handle any potential exception
                    var book = bookInfoProvider.GetBookInfo(bookId);
                    if (book == null || !book.isAvailable)
                    {
                        Console.WriteLine("Error: Book not available.");
                        return;
                    }

                    // Proceed with borrowing the book
                    bookManagement.BorrowBook(bookId, userId);
                    Console.WriteLine("Book borrowed");
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: Unable to fetch book information.");
                    // Handle the exception and prevent any further action
                }
            }
        }

        public void ReturnBookInteraction(int bookId)
        {
            try
            {
                bookManagement.ReturnBook(bookId);
                Console.WriteLine("Book successfully returned.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public void ReturnBooksInteraction(List<int> bookIds)
        {
            foreach (var bookId in bookIds)
            {
                try
                {
                    bookManagement.ReturnBook(bookId);
                    Console.WriteLine("Book successfully returned.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public bool IsValidReturnDate(string returnDate)
        {
            DateTime validDate;
            // Check if the return date is a valid DateTime and is not in the past
            if (DateTime.TryParse(returnDate, out validDate) && validDate >= DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}

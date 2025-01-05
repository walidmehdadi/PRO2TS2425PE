using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoanManagementSystem
{
    public class BookManagementStub : IBookManagement
    {
        private readonly IBookInfoProvider bookInfoProvider;
        public BookManagementStub(IBookInfoProvider bookInfoProvider)
        {
            this.bookInfoProvider = bookInfoProvider;
        }

        public void BorrowBook(int bookId, int userId)
        {
            // Get the book from BookInfoProvider
            Book book = bookInfoProvider.GetBookInfo(bookId);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            if (!book.IsAvailable())
            {
                Console.WriteLine("Sorry, the book is not available for borrowing.");
                return;
            }

            // Simulate borrowing by changing the book's availability status
            book.isAvailable = false;
            Console.WriteLine("Book successfully borrowed.");
        }

        public void ReturnBook(int bookId)
        {
            // Get the book from BookInfoProvider
            Book book = bookInfoProvider.GetBookInfo(bookId);
            if (book != null)
            {
                // Simulate returning by changing the book's availability status
                book.isAvailable = true;
            }
        }
    }
}

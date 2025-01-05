using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookLoanManagementSystem
{
    public class BookInfoProviderStub : IBookInfoProvider
    {
        private List<Book> books = new List<Book>
         {
            new Book { Id = 1, Title = "The Great Gatsby", isAvailable = true },
            new Book { Id = 2, Title = "1984", isAvailable = false },
            new Book { Id = 3, Title = "To Kill a Mockingbird", isAvailable = true },
            new Book { Id = 4, Title = "The Catcher in the Rye", isAvailable = true }
        };
        public string Url
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        // Return all books
        public List<Book> GetBooks()
        {
            return books;
        }

        // Return a single book based on its ID (for future flexibility)
        public Book GetBookInfo(int bookId)
        {
            var books = GetBooks();
            return books.Find(book => book.Id == bookId);
        }
    }
}

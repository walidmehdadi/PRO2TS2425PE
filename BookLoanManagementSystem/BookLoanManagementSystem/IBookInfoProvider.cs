using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoanManagementSystem
{
    public interface IBookInfoProvider
    {
        string Url { get; set; }
        public List<Book> GetBooks();
        public Book GetBookInfo(int bookId);
    }
}

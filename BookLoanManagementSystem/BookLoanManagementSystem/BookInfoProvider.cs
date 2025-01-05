using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoanManagementSystem
{
    public class BookInfoProvider : IBookInfoProvider
    {
        public string Url
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public List<Book> GetBooks()
        { 
            throw new NotImplementedException(); 
        }
        public Book GetBookInfo(int bookId)
        {
            throw new NotImplementedException();
        }
    }
}

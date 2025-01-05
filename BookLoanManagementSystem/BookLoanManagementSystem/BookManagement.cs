using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoanManagementSystem
{
    public class BookManagement : IBookManagement
    {
        private readonly IBookInfoProvider bookInfoProvider;
        public BookManagement(IBookInfoProvider bookInfoProvider)
        {
            this.bookInfoProvider = bookInfoProvider;
        }
        public void BorrowBook(int bookId, int userId)
        {
            throw new NotImplementedException();
        }

        public void ReturnBook(int bookId)
        {
            throw new NotImplementedException();
        }
    }
}

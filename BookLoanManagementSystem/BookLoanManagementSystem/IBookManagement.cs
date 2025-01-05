using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoanManagementSystem
{
    public interface IBookManagement
    {
        public void BorrowBook(int bookId, int userId);
        public void ReturnBook(int bookId);
    }
}

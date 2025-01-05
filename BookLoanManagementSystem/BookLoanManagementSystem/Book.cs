using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoanManagementSystem
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool isAvailable { get; set; }

        public bool IsAvailable()
        {
            return isAvailable;
        }
    }
}

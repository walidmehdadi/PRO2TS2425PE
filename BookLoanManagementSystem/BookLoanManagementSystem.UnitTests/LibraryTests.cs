using Moq;
using System.Net;
namespace BookLoanManagementSystem.UnitTests
{
    [TestClass]
    public sealed class LibraryTests
    {
        private const int UserId = 100;
        private string dateInThePast = "2025-01-01";
        private string dateInTheFuture = "2025-12-31";
        private List<Book> availableBooks = new List<Book>
         {
            new Book { Id = 1, Title = "The Great Gatsby", isAvailable = true },
            new Book { Id = 2, Title = "1984", isAvailable = true },
            new Book { Id = 3, Title = "To Kill a Mockingbird", isAvailable = true },
            new Book { Id = 4, Title = "The Catcher in the Rye", isAvailable = true }
        };
        private List<Book> unavailableBooks = new List<Book>
         {
            new Book { Id = 1, Title = "The Great Gatsby", isAvailable = false },
            new Book { Id = 2, Title = "1984", isAvailable = false },
            new Book { Id = 3, Title = "To Kill a Mockingbird", isAvailable = false },
            new Book { Id = 4, Title = "The Catcher in the Rye", isAvailable = false }
        };
        private List<int> availableIds;
        private List<int> unavailableIds;

        private Mock<IBookInfoProvider> bookInfoProviderMock = null;
        private Mock<IBookManagement> bookManagementMock = null;

        private Library library;

        [TestInitialize]
        public void Initialize()
        {
            bookInfoProviderMock = new Mock<IBookInfoProvider>();
            bookManagementMock = new Mock<IBookManagement>();

            availableIds = availableBooks.Select(book => book.Id).ToList();
            unavailableIds = unavailableBooks.Select(book => book.Id).ToList();
            library = new Library(bookInfoProviderMock.Object, bookManagementMock.Object);
        }

        [TestMethod]
        public void WhenBookIsAvailableCanBeBorrowed()
        {
            // --- Arrange ---
            bookInfoProviderMock.Setup(x => x.GetBookInfo(availableBooks[0].Id)).Returns(availableBooks[0]);

            // --- Act ---
            library.BorrowBookInteraction(availableBooks[0].Id, UserId, dateInTheFuture);

            // --- Assert ---
            bookManagementMock.Verify(x => x.BorrowBook(availableBooks[0].Id, UserId), Times.Once);
        }

        [TestMethod]
        public void WhenBookIsReturnedUpdatesAvailability()
        {
            // --- Arrange ---
            bookInfoProviderMock.Setup(x => x.GetBookInfo(unavailableBooks[0].Id)).Returns(unavailableBooks[0]);

            // --- Act ---
            library.ReturnBookInteraction(unavailableBooks[0].Id);

            // --- Assert ---
            bookManagementMock.Verify(x => x.ReturnBook(unavailableBooks[0].Id), Times.Once);
        }

        [TestMethod]
        public void WhenBookIsAlreadyBorrowedThrowsError()
        {
            // --- Arrange ---
            bookInfoProviderMock.Setup(x => x.GetBookInfo(unavailableBooks[0].Id)).Returns(unavailableBooks[0]);

            // --- Act ---
            library.BorrowBookInteraction(unavailableBooks[0].Id, UserId, dateInTheFuture);

            // --- Assert ---
            bookManagementMock.Verify(x => x.BorrowBook(unavailableBooks[0].Id, UserId), Times.Never);
        }

        [TestMethod]
        public void WhenBookHasInvalidReturnDateThrowsError()
        {
            // --- Arrange ---
            bookInfoProviderMock.Setup(x => x.GetBookInfo(availableBooks[0].Id)).Returns(availableBooks[0]);

            // --- Act ---
            library.BorrowBookInteraction(availableBooks[0].Id, UserId, dateInThePast);

            // --- Assert ---
            bookManagementMock.Verify(x => x.BorrowBook(availableBooks[0].Id, UserId), Times.Never);
        }

        [TestMethod]
        public void WhenFetchingBookInfoFailsExceptionIsCaught()
        {
            // --- Arrange ---
            bookInfoProviderMock.Setup(x => x.GetBookInfo(availableBooks[0].Id)).Throws<Exception>();

            // --- Act ---
            library.BorrowBookInteraction(availableBooks[0].Id, UserId, dateInTheFuture);

            // --- Assert ---
            bookManagementMock.Verify(x => x.BorrowBook(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void WhenMultipleBooksAreBorrowedTheyAreProcessedOneByOne()
        {
            // --- Arrange ---
            bookInfoProviderMock.Setup(x => x.GetBookInfo(It.IsAny<int>())).Returns<int>((id) => availableBooks.FirstOrDefault(b => b.Id == id));

            // --- Act ---
            library.BorrowBooksInteraction(availableIds, 100, "2025-01-10");

            // --- Assert ---
            // Verify that BorrowBook was called for each book one by one
            foreach (var bookId in availableIds)
            {
                bookManagementMock.Verify(x => x.BorrowBook(bookId, 100), Times.Once);
            }
        }

        [TestMethod]
        public void WhenMultipleBooksAreReturnedAllAreMarkedAsAvailable()
        {
            // --- Arrange ---
            bookInfoProviderMock.Setup(x => x.GetBookInfo(It.IsAny<int>())).Returns<int>((id) => unavailableBooks.FirstOrDefault(b => b.Id == id));

            // Act:
            library.ReturnBooksInteraction(unavailableIds);

            // Assert: Verify that each book's ReturnBook method was called and the availability was updated
            foreach (var bookId in unavailableIds)
            {
                bookManagementMock.Verify(x => x.ReturnBook(bookId), Times.Once);
            }
        }
    }
}

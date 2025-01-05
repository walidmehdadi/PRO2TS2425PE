using BookLoanManagementSystem;

Console.WriteLine("Starting the Book Loan Management System...");

// Initialize the dependencies
IBookInfoProvider bookInfoProvider = new BookInfoProviderStub();
IBookManagement bookManagement = new BookManagementStub(bookInfoProvider);

// Initialize the library
Library library = new Library(bookInfoProvider, bookManagement);

// Simulate the system running continuously
while (true)
{
    try
    {
        library.Work();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }

    // Wait for 5 seconds before the next cycle
    Thread.Sleep(5000);
}

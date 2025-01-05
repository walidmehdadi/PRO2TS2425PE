using BookLoanManagementSystem;

Console.WriteLine("Starting the Book Loan Management System...");

// Initialize the dependencies
IBookInfoProvider bookInfoProvider = new BookInfoProviderStub();
IBookManagement bookManagement = new BookManagementStub(bookInfoProvider);

// Initialize the library
Library library = new Library(bookInfoProvider, bookManagement);

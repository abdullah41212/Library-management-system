using Microsoft.EntityFrameworkCore;
using Library_management_system.Models.Database;
using Library_management_system.Models.Database.Tables;
using Library_management_system.DTOs;
using CustomExceptionsNS = Library_management_system.Models.Exceptions;
using Library_management_system.Models.database;
using System.Xml;
using Microsoft.AspNetCore.Http.HttpResults;
using Library_management_system.Models.Response;
using Library_management_system.Enums;

namespace Library_management_system.Services
{
    public class bookServices
    {

        private readonly AppDbContext _context;

        public bookServices(AppDbContext context) {
            _context = context;
        }


        public async Task<List<UpdateBookDTO>> GetAllBooks() {
            var books = await _context.Books.Select(b=> new UpdateBookDTO { 
            id=b.Id,
            Title=b.Title,
            Author=b.Author,
            CopiesAvailable=b.CopiesAvailable,
            CopiesTotal=b.CopiesTotal,
            ISBN=b.ISBN
            }).ToListAsync();
            return books;
        }
        public async Task<AddBookResponseDTO> AddBooks(AddBookDTO dto) {

            if (dto.Title == null || dto.ISBN == null || dto.Author == null || dto.CopiesTotal <= 0 || dto.CopiesAvailable <= 0) {
                throw new CustomExceptionsNS.CustomExceptions("invalid input", 400);
            }

            var book = new Book {
                Title = dto.Title,
                ISBN = dto.ISBN,
                CopiesAvailable = dto.CopiesAvailable,
                CopiesTotal = dto.CopiesTotal,
                Author = dto.Author
            };
            var result = await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return new AddBookResponseDTO
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                CopiesAvailable = book.CopiesAvailable,
                CopiesTotal = book.CopiesTotal,
                Author = book.Author
            };
        }

        public async Task DeleteBook(int bookID) {
            if (bookID<=0) {
                throw new CustomExceptionsNS.CustomExceptions("Please Provide BookID", 400);
            }
            var book = await _context.Books.FirstOrDefaultAsync(id => id.Id == bookID);
            if (book == null) {
                throw new CustomExceptionsNS.CustomExceptions("Book Not Found", 400);
            }
            book.BookStatus = (int)BookStatuses.deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<UpdateBookDTO> UpdateBook(UpdateBookDTO dto)
        {
            var book = await _context.Books.FirstOrDefaultAsync(id => id.Id == dto.id);

            if (book == null) {
                throw new CustomExceptionsNS.CustomExceptions("Book Not Found", 400);
            }
            book.Author = dto.Author;
            book.ISBN = dto.ISBN;
            book.CopiesAvailable = dto.CopiesAvailable;
            book.CopiesTotal = dto.CopiesTotal;
            book.Title = dto.Title;

            await _context.SaveChangesAsync();
            return new UpdateBookDTO
            {
                Author = dto.Author,
                ISBN = dto.ISBN,
                CopiesAvailable = dto.CopiesAvailable,
                CopiesTotal = dto.CopiesTotal,
                Title = dto.Title

            };

        }
        public async Task<Loans> LoanBook(LoanBookDTO dto,int userId )
        {
            var book = await _context.Books.FirstOrDefaultAsync(id => id.Id == dto.BookId);

            if (book == null)
            {
                throw new CustomExceptionsNS.CustomExceptions("Book Not Found", 400);
            }
            if (book.CopiesAvailable == 0) {
                throw new CustomExceptionsNS.CustomExceptions("Book Unavailable", 400);
            }

            var loan = new Loans
            {
                BookId = dto.BookId,
                Book = book,
                DueDate = dto.DueDate,
                UserId = userId

            };
            book.CopiesAvailable -= 1;
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
            return new Loans
            {
                Id=loan.Id,
                BookId = dto.BookId,
                Book = book,
                DueDate = dto.DueDate,
                UserId = userId

            };

        }
        public async Task<Loans> ReturnBook(int loanid, int userId)
        {
            var loan = await _context.Loans.FirstOrDefaultAsync(id => id.Id == loanid);
            if (loan == null) {
                throw new CustomExceptionsNS.CustomExceptions("Loan Not Found", 400);
            }
            if (loan.UserId != userId) {
                throw new CustomExceptionsNS.CustomExceptions("Book Not Found", 400);
            }
            var book = await _context.Books.FirstOrDefaultAsync(id => id.Id == loan.BookId);
            if (loan.ReturnedDate != null) {
                throw new CustomExceptionsNS.CustomExceptions("Loan has already been returned", 400);
            }
            if (book == null) {
                throw new CustomExceptionsNS.CustomExceptions("Book Not Found", 400);
            }
            book.CopiesAvailable += 1;
            loan.ReturnedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return new Loans
            {
                Id = loan.Id,
                BookId = loan.BookId,
                Book = book,
                UserId = userId

            };

        }


    }
}

using backend.DTOS;
using backend.Interfaces;
using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class bookServices : IBook
    {
        private readonly TestDBContext context;
        public bookServices(TestDBContext Dbcontext)
        {
            this.context = Dbcontext;
        }
        public async Task<Book> CreateBook(AddBook addbook)
        {
            var newBook = new Book
            {
                BookId = Guid.NewGuid(),
                BookName = addbook.BookName,
                BookAuthor = addbook.BookAuthor,
                BookDescription = addbook.BookDescription,
                ImgUrl = addbook.ImgUrl,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            context.Books.Add(newBook);
            await context.SaveChangesAsync();
            return newBook;
        }

        public async Task DeleteBook(Book book)
        {
            context.Remove(book);
            await context.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAll()
        {
            var bookList = await context.Books.ToListAsync();
            return bookList;
        }

        public async Task<Book> GetById(Guid id)
        {
            var findBook = await context.Books.FirstOrDefaultAsync(e => e.BookId == id);
            return findBook;
        }

        public async Task UpdateBook(Book book, AddBook addbook)
        {
            book.BookName = addbook.BookName;
            book.BookAuthor = addbook.BookAuthor;
            book.BookDescription = addbook.BookDescription;
            book.ImgUrl = addbook.ImgUrl;
            book.UpdatedAt = DateTime.Now;
            await context.SaveChangesAsync();
        }
    }
}

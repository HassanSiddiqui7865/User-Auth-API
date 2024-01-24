using backend.DTOS;
using backend.Model;

namespace backend.Interfaces
{
    public interface IBook
    {
        Task<Book> CreateBook(AddBook addbook);

        Task<Book> GetById(Guid id);

        Task<List<Book>> GetAll();
        Task UpdateBook(Book book,AddBook addbook);

        Task DeleteBook(Book book);
    }
}

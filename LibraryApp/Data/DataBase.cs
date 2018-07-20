using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using LibraryApp.Models;

namespace LibraryApp.Data
{
    public class DataBase:IDataStore<Author>
    {
       public  List<Author> Aut = new List<Author>();
        
        public async Task<bool> AddDataAsync(Author author)
        {
            Aut.Add(author);
            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteDataAsync(Author author)
        {
            var _aut =Aut.Where((Author arg) => arg.AuthorId == author.AuthorId).FirstOrDefault();
                 Aut.Remove(_aut);

            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateDataAsync(Author author)
        {
            var _aut = Aut.Where((Author arg) => arg.AuthorId == author.AuthorId).FirstOrDefault();
                Aut.Remove(_aut);
                     Aut.Add(author);
            return await Task.FromResult(true);
        }
        public async Task<Author> GetDataAsync(string id)
        {
            return await Task.FromResult(Aut.FirstOrDefault(s => s.AuthorId == id));
        }
        public async Task<IEnumerable<Author>> GetDataAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(Aut);
        }
    }
    public class BookDataBase : IDataStore<Book>
    {
        public List<Book> Book = new List<Book>();
       
        public async Task<bool> AddDataAsync(Book book)
        {
            Book.Add(book);
            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteDataAsync(Book book)
        {
            var _book = Book.Where((Book arg) => arg.BookId == book.BookId).FirstOrDefault();
                Book.Remove(_book);

            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateDataAsync(Book book)
        {
            var _aut = Book.Where((Book arg) => arg.BookId == book.BookId).FirstOrDefault();
              Book.Remove(_aut);
                Book.Add(book);
            return await Task.FromResult(true);
        }
        public async Task<Book> GetDataAsync(string id)
        {
            return await Task.FromResult(Book.FirstOrDefault(s => s.BookId == id));
        }
        public async Task<IEnumerable<Book>> GetDataAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(Book);
        }
    }
}

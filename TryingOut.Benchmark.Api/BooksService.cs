using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using TryingOut.Benchmark.Api.Grpc;

namespace TryingOut.Benchmark.Api
{
    public class BooksService : BookService.BookServiceBase
    {
        private static readonly List<Book> Books = CreateBooks();

        private static List<Book> CreateBooks()
        {
            return BookRepository.Books.Select(MapBook).ToList();

            Book MapBook(BookRepository.BookModel x)
            {
                return new Book
                {
                    ISBN = x.ISBN,
                    Title = x.Title,
                    YearPublished = x.YearPublished,
                    Author = new Author
                    {
                        FirstName = x.Author.FirstName,
                        LastName = x.Author.LastName
                    }
                };
            }
        }

        public override Task<BooksResponse> GetAll(GetAllBooksRequest request, ServerCallContext context)
        {
            var response = new BooksResponse();
            response.Books.AddRange(Books);
            return Task.FromResult(response);
        }

        public override Task<BookResponse> First(GetFirstBookRequest request, ServerCallContext context)
        {
            var response = new BookResponse();
            response.Book = Books[0];
            return Task.FromResult(response);
        }
    }
}

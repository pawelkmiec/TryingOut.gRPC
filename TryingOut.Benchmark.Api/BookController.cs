using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TryingOut.Benchmark.Contracts;

namespace TryingOut.Benchmark.Api
{
    [Route("books")]
    public class BookController : Controller
    {
        private static readonly List<BookDto> Books = CreateBooks();

        private static List<BookDto> CreateBooks()
        {
            return BookRepository.Books.Select(MapBook).ToList();

            BookDto MapBook(BookRepository.BookModel x)
            {
                return new BookDto
                {
                    ISBN = x.ISBN,
                    Title = x.Title,
                    YearPublished = x.YearPublished,
                    Author = new AuthorDto
                    {
                        FirstName = x.Author.FirstName,
                        LastName = x.Author.LastName
                    }
                };
            }
        }

        [Route("all")]
        public List<BookDto> GetAll()
        {
            return Books;
        }

        [Route("first")]
        public BookDto First()
        {
            return Books[0];
        }
    }
}

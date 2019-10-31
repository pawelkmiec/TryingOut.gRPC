using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TryingOut.Benchmark.Contracts;

namespace TryingOut.Benchmark.Api
{
    [Route("books")]
    public class BookController : Controller
    {
        [Route("all")]
        public List<BookDto> GetAll()
        {
            return BookRepository.Books.Select(MapBook).ToList();
        }

        [Route("first")]
        public BookDto First()
        {
            return MapBook(BookRepository.Books[0]);
        }

        [Route("stats")]
        public BooksSalesStatsDto Stats()
        {
            return MapStats(BookRepository.Stats);
        }

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

        private BooksSalesStatsDto MapStats(BookRepository.BookSalesStatsModel stats)
        {
            return new BooksSalesStatsDto
            {
                SalesQuantityTotal = stats.SalesQuantityTotal,
                SalesQuantityLastMonth = stats.SalesQuantityLastMonth,
                SalesQuantityLastYear = stats.SalesQuantityLastYear
            };
        }
    }
}

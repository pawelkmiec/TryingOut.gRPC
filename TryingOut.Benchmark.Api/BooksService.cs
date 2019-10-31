using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using TryingOut.Benchmark.Api.Grpc;

namespace TryingOut.Benchmark.Api
{
    public class BooksService : BookService.BookServiceBase
    {
        public override Task<BooksResponse> GetAll(GetAllBooksRequest request, ServerCallContext context)
        {
            var books = BookRepository.Books.Select(MapBook).ToList();
            var response = new BooksResponse();
            response.Books.AddRange(books);
            return Task.FromResult(response);
        }

        public override Task<BookResponse> First(GetFirstBookRequest request, ServerCallContext context)
        {
            var response = new BookResponse
            {
                Book = MapBook(BookRepository.Books[0])
            };
            return Task.FromResult(response);
        }

        public override Task<GetSalesStatsResponse> GetSalesStats(GetSalesStatsRequest request, ServerCallContext context)
        {
            var response = new GetSalesStatsResponse
            {
                Stats = MapStats(BookRepository.Stats)
            };
            return Task.FromResult(response);
        }

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

        BookSalesStats MapStats(BookRepository.BookSalesStatsModel stats)
        {
            return new BookSalesStats
            {
                SalesQuantityTotal = stats.SalesQuantityTotal,
                SalesQuantityLastMonth = stats.SalesQuantityLastMonth,
                SalesQuantityLastYear = stats.SalesQuantityLastYear
            };
        }
    }
}

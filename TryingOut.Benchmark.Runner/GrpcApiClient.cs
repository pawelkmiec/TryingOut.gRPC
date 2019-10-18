using System.Collections.Generic;
using System.Threading.Tasks;
using TryingOut.Benchmark.Api.Grpc;

namespace TryingOut.Benchmark.Runner
{
    public class GrpcApiClient
    {
        private readonly BookService.BookServiceClient _client;

        public GrpcApiClient(string apiUrl)
        {
            var channel = Grpc.Net.Client.GrpcChannel.ForAddress(apiUrl);
            _client = new BookService.BookServiceClient(channel);
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var response = await _client.GetAllAsync(new GetAllBooksRequest());
            return response.Books;
        }

        public async Task<Book> GetBook()
        {
            var response = await _client.FirstAsync(new GetFirstBookRequest());
            return response.Book;
        }
    }
}

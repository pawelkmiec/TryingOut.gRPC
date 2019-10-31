using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TryingOut.Benchmark.Contracts;

namespace TryingOut.Benchmark.Runner
{
    public class JsonApiClient
    {
        private readonly HttpClient _client;

        public JsonApiClient(string apiUrl)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(apiUrl)
            };
        }

        public async Task<IEnumerable<BookDto>> GetAllBooks()
        {
            var response = await _client.GetStringAsync("/books/all");
            var books = JsonSerializer.Deserialize<IEnumerable<BookDto>>(response);
            return books;
        }

        public async Task<BookDto> GetBook()
        {
            var response = await _client.GetStringAsync("/books/first");
            var book = JsonSerializer.Deserialize<BookDto>(response);
            return book;
        }

        public async Task<BooksSalesStatsDto> GetStats()
        {
            var response = await _client.GetStringAsync("/books/stats");
            var stats = JsonSerializer.Deserialize<BooksSalesStatsDto>(response);
            return stats;
        }
    }
}

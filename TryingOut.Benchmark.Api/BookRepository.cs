﻿using System.Collections.Generic;
using System.Linq;
using AutoFixture;

namespace TryingOut.Benchmark.Api
{
    // this repo is to ensure that both GRPC & JSON APIs use the same set of data, initialized once
    public class BookRepository
    {
        public static readonly List<BookModel> Books = CreateBooks();
        
        public static readonly BookSalesStatsModel Stats = CreateBookSalesStats();

        private static List<BookModel> CreateBooks()
        {
            var fixture = new Fixture();
            return Enumerable.Range(0, 1000).Select(s => fixture.Create<BookModel>()).ToList();
        }

        private static BookSalesStatsModel CreateBookSalesStats()
        {
            return new BookSalesStatsModel
            {
                SalesQuantityLastMonth = 25,
                SalesQuantityLastYear = 289,
                SalesQuantityTotal = 1561
            };
        }

        public class BookModel
        {
            public string ISBN { get; set; }

            public string Title { get; set; }

            public int YearPublished { get; set; }

            public AuthorModel Author { get; set; }
        }

        public class AuthorModel
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        public class BookSalesStatsModel
        {
            public int SalesQuantityLastMonth { get; set; }

            public int SalesQuantityLastYear { get; set; }

            public int SalesQuantityTotal { get; set; }
        }
    }
}

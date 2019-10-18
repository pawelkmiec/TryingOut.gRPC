namespace TryingOut.Benchmark.Contracts
{
    public class BookDto
    {
        public string ISBN { get; set; }
        
        public string Title { get; set; }

        public int YearPublished { get; set; }

        public AuthorDto Author { get; set; }
    }
}
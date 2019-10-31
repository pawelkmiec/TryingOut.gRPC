using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TryingOut.Benchmark.Runner
{
    public class Program
    {
        const string ApiUrl = "https://localhost:60616";
        private static readonly GrpcApiClient GrpcClient = new GrpcApiClient(ApiUrl);
        private static readonly JsonApiClient JsonClient = new JsonApiClient(ApiUrl);

        static async Task Main()
        {
            await RunTest(nameof(GetAllBooksGrpc), GetAllBooksGrpc);
            await RunTest(nameof(GetAllBooksJson), GetAllBooksJson);
            
            await RunTest(nameof(GetBookGrpc), GetBookGrpc);
            await RunTest(nameof(GetBookJson), GetBookJson);

            await RunTest(nameof(GetStatsGrpc), GetStatsGrpc);
            await RunTest(nameof(GetStatsJson), GetStatsJson);

            if (Debugger.IsAttached)
            {
                Console.ReadLine();
            }
        }

        private static async Task RunTest(string methodName, Func<Task> action)
        {
            Console.WriteLine($"{methodName} - start");
            var responseTime = await Repeat(100, action);
            Console.WriteLine($"{methodName} - finish. Total ms: {responseTime}");
        }

        private static async Task GetAllBooksGrpc()
        {
            await GrpcClient.GetAllBooks();
        }

        private static async Task GetAllBooksJson()
        {
            await JsonClient.GetAllBooks();
        }

        private static async Task GetBookGrpc()
        {
            await GrpcClient.GetBook();
        }

        private static async Task GetBookJson()
        {
            await JsonClient.GetBook();
        }

        private static async Task GetStatsGrpc()
        {
            await GrpcClient.GetStats();
        }

        private static async Task GetStatsJson()
        {
            await JsonClient.GetStats();
        }

        private static async Task<long> Repeat(int count, Func<Task> toRepeat)
        {
            var sw = new Stopwatch();
            sw.Start();

            for (var i = 0; i < count; i++)
            {
                await toRepeat();
            }

            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}

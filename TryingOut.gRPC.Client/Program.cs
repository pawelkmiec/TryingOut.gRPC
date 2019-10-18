using System;
using System.Threading.Tasks;
using TryingOut.gRPC.Service;

namespace TryingOut.gRPC.Client
{
    class Program
    {
        static async Task Main()
        {
            var channel = Grpc.Net.Client.GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var response = await client.SayHelloAsync(new HelloRequest
            {
                Name = "Paweł"
            });
            Console.WriteLine($"Response from server: {response.Message}");
        }
    }
}
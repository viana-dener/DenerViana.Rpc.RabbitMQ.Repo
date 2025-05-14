using Polly;
using Polly.Retry;
using Polly.Extensions.Http;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Policy;

public static class RetryWaitPolicy
{
    public static AsyncRetryPolicy<HttpResponseMessage> GetRetryWaitPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(new[]
            {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
            }, (outcome, timeSpan, retryCount, contex) =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Trying for {retryCount} time!");
                Console.ForegroundColor = ConsoleColor.White;
            });
    }
}

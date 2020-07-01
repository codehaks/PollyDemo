using Polly;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Common
{
    public class TestClient
    {
        private readonly HttpClient _client;
        private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;

        public TestClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://codehaks1.com");

            //_circuitBreakerPolicy = Policy.Handle<Exception>()
            //   .CircuitBreakerAsync(3, TimeSpan.FromMinutes(1));

        }

        public async Task<string> GetContent()
        {
            //await _circuitBreakerPolicy.ExecuteAsync(async () =>
            //{
                var result = await _client.GetAsync("/");
            //    return await result.Content.ReadAsStringAsync();
            //});

            return "done";

        }
    }
}

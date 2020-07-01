using Microsoft.Extensions.Logging;
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
        private readonly ILogger<TestClient> _logger;

        public TestClient(HttpClient client, ILogger<TestClient> logger)
        {
            _client = client;
            _logger = logger;

            _client.BaseAddress = new Uri("http://localhost:5005/api/");

            //_circuitBreakerPolicy = Policy.Handle<Exception>()
            //   .CircuitBreakerAsync(3, TimeSpan.FromMinutes(1));

        }

        public async Task<string> GetContent()
        {
            //await _circuitBreakerPolicy.ExecuteAsync(async () =>
            //{
                var result = await _client.GetAsync("test");
            //    return await result.Content.ReadAsStringAsync();
            //});

            return await result.Content.ReadAsStringAsync();

        }
    }
}

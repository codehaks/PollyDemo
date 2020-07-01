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

        }

        public async Task<string> GetContent()
        {

            var result = await _client.GetAsync("test");
            return await result.Content.ReadAsStringAsync();


        }
    }
}

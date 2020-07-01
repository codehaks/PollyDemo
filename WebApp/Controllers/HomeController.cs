using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly;
using WebApp.Common;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TestClient _client;
        public HomeController(ILogger<HomeController> logger, TestClient client)
        {
            _logger = logger;
            _client = client;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("retry")]
        public async Task<IActionResult> Retry()
        {
            var polly = Policy.Handle<Exception>()
                .WaitAndRetryAsync(2, sleep => TimeSpan.FromSeconds(1));
            var client = new HttpClient();

            await polly.ExecuteAsync(async () =>
            {
                _logger.LogInformation("Retrying ...");
                var result = await client.GetAsync("notfound");
            });
            return Ok("Done");
        }

        [Route("breaker")]
        public async Task<IActionResult> Breaker()
        {
            var result=await _client.GetContent();

            return Ok(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

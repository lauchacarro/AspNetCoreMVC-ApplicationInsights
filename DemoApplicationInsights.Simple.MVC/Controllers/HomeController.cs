using DemoApplicationInsights.Simple.MVC.Models;

using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace DemoApplicationInsights.Simple.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TelemetryClient _telemetryClient;

        public HomeController(ILogger<HomeController> logger, TelemetryClient telemetryClient)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
        }

        public IActionResult Index()
        {
            _telemetryClient.TrackEvent("Custom Event in Index");

            return View();
        }

        public IActionResult Privacy()
        {
            // Los Logs se guardan como Trace

            _logger.LogWarning("Privacy Policy is obsolete");

            return View();
        }

        public IActionResult Boom()
        {
            throw new NotImplementedException("Boom");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
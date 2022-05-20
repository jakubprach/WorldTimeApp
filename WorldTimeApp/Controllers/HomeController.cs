using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorldTimeApp.Models;

namespace WorldTimeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHttpClientFactory _fac;
        private readonly IConfiguration _config;


        public HomeController(ILogger<HomeController> logger, IHttpClientFactory fac, IConfiguration config)
        {
            _logger = logger;
            _fac = fac;
            _config = config;
        }

        public async Task<string> Time()
        {
            TimeService timeService
                = new TimeService(_fac, _config);

            var time = await timeService.GetTime();

            return time;
        }

        public async Task<IActionResult> ListTimeZones()
        {
            TimezoneList timezoneList = new TimezoneList(_fac, _config);

            var list = await timezoneList.GetTime();

            ViewBag.TimeZones = list;

            return View();
        }

        public async Task<ActionResult> Index()
        {
            TimeService timeService
                = new TimeService(_fac, _config);

            var time = await timeService.GetTime();

            if (time == "error")
            {
                return RedirectToAction("ListTimeZones");
            }

            ViewBag.Time = time;
            ViewBag.Location = _config.GetValue<string>(
                "Endpoint"
                );

            return View();
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JobBoardTracker.Models;
using System.Data.SqlClient;
using System.Net.Http;
using Models;

namespace JobBoardTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> JobBoardAsync()
        {
            string url = "https://localhost:44349/JobBoards";
            JobBoardViewModel jobBoards = new JobBoardViewModel();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                jobBoards = new JobBoardViewModel(response);
            }

            return View(jobBoards);
        }

        public async Task<IActionResult> JobsAsync(string source)
        {
            JobsViewModel jobs = new JobsViewModel();

            if (source != null || source != "")
            {
                string url = "https://localhost:44349/Jobs?source=" + source;

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(url);
                    jobs = new JobsViewModel(response);
                    jobs.source = source;
                }
            }
            else
            {
                jobs.errorMessage = "something went wrong";
            }

            return View(jobs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

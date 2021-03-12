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
using Microsoft.Extensions.Configuration;

namespace JobBoardTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IConfiguration _iconfiguration;

        public HomeController(ILogger<HomeController> logger, IConfiguration iconfiguration)
        {
            _logger = logger;
            _iconfiguration = iconfiguration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> JobBoardAsync()
        {
            string url = _iconfiguration["apiPath"] + "JobBoards";
            JobBoardViewModel jobBoards = new JobBoardViewModel();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(url);
                    jobBoards = new JobBoardViewModel(response);
                }
            }
            catch
            {
                jobBoards.errorMessage = "something went wrong";
            }

            return View(jobBoards);
        }

        public async Task<IActionResult> JobsAsync(string source)
        {
            JobsViewModel jobs = new JobsViewModel();

            try
            {
                if (source != null || source != "")
                {
                    string url = _iconfiguration["apiPath"] + "Jobs?source=" + source;

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
            }
            catch
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

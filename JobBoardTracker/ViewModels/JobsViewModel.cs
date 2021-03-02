using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace JobBoardTracker.Models
{
    public class JobsViewModel
    {
        private string _json;
        public JobsViewModel(string json)
        {
            _json = json;
            jobLst = JsonConvert.DeserializeObject<List<JobSourceResolutionModel>>(_json.ToString());
        }
        public JobsViewModel() { }
        public List<JobSourceResolutionModel> jobLst { get; set; }
        public string source { get; set; }
        public string errorMessage { get; set; }
    }
}

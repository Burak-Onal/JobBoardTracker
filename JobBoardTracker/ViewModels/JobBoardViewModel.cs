using Newtonsoft.Json;
using System.Collections.Generic;
using Models;

namespace JobBoardTracker.Models
{
    public class JobBoardViewModel
    {
        private string _json;
        public JobBoardViewModel(string json)
        {
            _json = json;
            jobBoardLst = JsonConvert.DeserializeObject<List<JobBoardModel>>(_json.ToString());
        }
        public JobBoardViewModel() { }
        public List<JobBoardModel> jobBoardLst { get; set; }
    }
}

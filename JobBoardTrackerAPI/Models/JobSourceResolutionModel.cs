using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoardTrackerAPI.Models
{
    public class JobSourceResolutionModel
    {
        public string ID { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string JobURL { get; set; }
        public string Source { get; set; }
    }
}

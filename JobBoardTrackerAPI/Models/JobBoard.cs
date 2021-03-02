using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoardTrackerAPI.Models
{
    public class JobBoard
    {
        public string Name { get; set; }
        public string Rating { get; set; }
        public string Root_domain { get; set; }
        public string Logo_file { get; set; }
        public string Description { get; set; }
    }
}

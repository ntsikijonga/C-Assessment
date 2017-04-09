using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tangent.Models
{
    public class HomeViewModel
    {
        public string token { get; set; }
        public Project Project { get; set; }
        public IList<Project> ProjectList { get; set; }

    }
}
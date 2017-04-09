using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tangent.Models
{
    public class Project
    {
        public string pk { get; set; }
        [Required(ErrorMessage = "Tittle is required")]
        public string title { get; set; }
        [Required(ErrorMessage = "Description is required is required")]
        public string description { get; set; }
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string start_date { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd")]
        public string end_date { get; set; }
        public bool is_billable { get; set; }
        public bool is_active { get; set; }
        public IList task_set { get; set; }
    }
}
using JobPortal.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.Web.Areas.JobView.ViewModels
{
    public class ShowJobViewModel
    {
        [Required(ErrorMessage ="Cannot be empty")]
        [Display(Name =" Job Category" )]

        public int JobCategoryId { get; set; }


        public ICollection<JobDetail> JobDetail { get; set; }
    }
}

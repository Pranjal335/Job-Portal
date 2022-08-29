using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace JobPortal.Web.Models
{
    [Table(name: "JobDetails")]
    public class JobDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CompanyId { get; set; }

        // Company Name

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Name of the Company")]

        public string CompanyName { get; set; }


        // Experience required

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Display(Name = "Required Experience")]

        public int Experience { get; set; }


        // CTC


        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "CTC")]

        public string CTC { get; set; }

        //Location

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Column(TypeName = "varchar(20)")]
        [Display(Name = "Location")]

        public string Location { get; set; }


        // Job Discription

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Job Discription")]

        public string JobDiscription { get; set; }



        #region Navigation Properties to the JobCategory Model
        [Display(Name = "Choose Category")]
        public int JobCategoryId { get; set; }


        [ForeignKey(nameof(JobDetail.JobCategoryId))]
        public JobCategory JobCategory { get; set; }
        #endregion

        #region Navigation property to CandidateDetail Model

        public ICollection<CandidateDetail> CandidateDetail { get; set; }

        #endregion


       


    }
}

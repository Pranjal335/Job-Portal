using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Web.Models
{
    [Table(name:"CandidateDetails")]
    public class CandidateDetail
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CandidateDetailId { get; set; }


        // Candidate First Name

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "First Name")]

        public string CandidateFirstName { get; set; }

        //Candidate Last Name

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Last Name")]

        public string CandidateLastName { get; set; }


        // Email

        [Required(ErrorMessage = "Email ID required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email ID")]

        public string CandidateEmailId { get; set; }


        // Date of Birth

        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateofBirth { get; set; }

        // Experience

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Column(TypeName = "varchar(10)")]
        [Display(Name = "Experience")]

        //Resume

        public string CandidateExperience { get; set; }



        #region Navigation Properties to the JobCategory Model

        public int JobDetailId { get; set; }


        [ForeignKey(nameof(CandidateDetail.JobDetailId))]
        public JobDetail JobDetail { get; set; }
        #endregion




    }
}

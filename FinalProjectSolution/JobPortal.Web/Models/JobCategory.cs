using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Web.Models
{
    [Table(name: "JobCategories")]
    public class JobCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int JobCategoryId { get; set; }


        //Name of Category

        [Required]
        [StringLength(50)]
        public string JobCategoryName { get; set; }


        #region Navigation property to JobDetails Model

        public ICollection<JobDetail> JobDetail { get; set; }

        #endregion

        

    }
}

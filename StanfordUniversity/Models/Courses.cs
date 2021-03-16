using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StanfordUniversity.Models
{
    public class Courses
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int CourseID { get; set; }
        [Required]
        [StringLength(50)]
        public String Name { get; set; }
        [StringLength(1000)]
        public String Description { get; set; }

        public virtual ICollection<Groups> Groups { get; set; }
    }
}

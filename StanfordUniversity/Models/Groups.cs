using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StanfordUniversity.Models
{
    public class Groups
    {
        [Key]
        public int GroupID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        [StringLength(25)]
        public String Name { get; set; }

        public Courses Courses { get; set; }
        public ICollection<Students> Students { get; set; }
    }
}

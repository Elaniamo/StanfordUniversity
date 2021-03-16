using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StanfordUniversity.Models
{
    public class Students
    {
        [Key]
        public int StudentID { get; set; }
        [Required]
        public virtual int GroupID { get; set; }
        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public Groups Groups { get; set; }

    }
}

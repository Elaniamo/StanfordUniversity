using StanfordUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StanfordUniversity.ViewModels
{
    public class CoursesViewModel
    {
        public int CourseID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public virtual ICollection<GroupsViewModel> Groups { get; set; }

    }
}

using StanfordUniversity.Models;
using System;
using System.Collections.Generic;

namespace StanfordUniversity.ViewModels
{
    public class GroupsViewModel
    {
        public int GroupID { get; set; }
        public int CourseID { get; set; }
        public String Name { get; set; }

        public Courses Courses { get; set; }
        public ICollection<StudentsViewModel> Students { get; set; }
    }
}

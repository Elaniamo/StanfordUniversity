using StanfordUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StanfordUniversity.ViewModels
{
    public class StudentsViewModel
    {
        public int StudentID { get; set; }
        public int GroupID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GroupsViewModel Groups { get; set; }
    }
}

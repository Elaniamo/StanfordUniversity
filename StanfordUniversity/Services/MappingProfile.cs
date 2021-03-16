using AutoMapper;
using StanfordUniversity.Models;
using StanfordUniversity.ViewModels;

namespace StanfordUniversity.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Courses, CoursesViewModel>();
            CreateMap<Groups, GroupsViewModel>();
            CreateMap<Students, StudentsViewModel>();
        }
    }
}

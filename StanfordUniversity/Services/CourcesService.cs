using AutoMapper;
using StanfordUniversity.Data;
using StanfordUniversity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StanfordUniversity.Models
{
    public class CourcesService
    {
        private readonly SchoolContext _context;
        private readonly IMapper _mapper;

        public CourcesService(SchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CoursesViewModel>> GetIndexAsync()
        {
            var courses = await _context.Courses.ToListAsync();
            return _mapper.Map<IEnumerable<Courses>, IEnumerable<CoursesViewModel>>(courses);
        }

        public CoursesViewModel GetDetails(int? id)
        {
            var courses = _context.Courses
                .Include(g => g.Groups.OrderBy(i => i.Name))
                .AsNoTracking()
                .FirstOrDefault(m => m.CourseID == id);

            return _mapper.Map<Courses, CoursesViewModel>(courses);
        }

        public CoursesViewModel Create([Bind("CourseID,Name,Description")] Courses courses)
        {
            _context.Add(courses);
            _context.SaveChanges();

            return _mapper.Map<Courses, CoursesViewModel>(courses);
        }

        public CoursesViewModel Edit(int? id)
        {
            var courses = _context.Courses.Find(id);
            return _mapper.Map<Courses, CoursesViewModel>(courses);
        }

        public CoursesViewModel Edit([Bind("CourseID,Name,Description")] Courses courses)
        {
            if (!CoursesExists(courses.CourseID))
                return null;

            try
            {
                _context.Update(courses);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return _mapper.Map<Courses, CoursesViewModel>(courses);
        }

        private bool CoursesExists(int id)
        {
            return _context.Courses.Any(e => e.CourseID == id);
        }
    }
}

using AutoMapper;
using StanfordUniversity.Data;
using StanfordUniversity.Models;
using StanfordUniversity.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StanfordUniversity.Services
{
    public class StudentsService
    {
        private readonly SchoolContext _context;
        private readonly IMapper _mapper;

        public StudentsService(SchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<StudentsViewModel> GetSortStudents(string sortOrder)
        {
            var students = from s in _context.Students
                           select s;
            students = sortOrder switch
            {
                "name_desc" => students.OrderByDescending(s => s.FirstName),
                "Group" => students.OrderBy(s => s.GroupID),
                "Group_desc" => students.OrderByDescending(s => s.GroupID),
                _ => students.OrderBy(s => s.FirstName),
            };
            return _mapper.Map<IEnumerable<Students>, IEnumerable<StudentsViewModel>>(students);
        }

        public async Task<StudentsViewModel> DetailsAsync(int? id)
        {
            var students = await _context.Students
                .Include(g => g.Groups)
                    .ThenInclude(e => e.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.StudentID == id);
            return _mapper.Map<Students, StudentsViewModel>(students);
        }

        public async Task<StudentsViewModel> CreateAsync(
            Students students)
        {
            try
            {
                _context.Add(students);
                await _context.SaveChangesAsync();
                return null;
            }
            catch (DbUpdateException)
            {
                return _mapper.Map<Students, StudentsViewModel>(students);
            }
        }

        public async Task<StudentsViewModel> EditAsync(int? id)
        {
            return _mapper.Map<Students, StudentsViewModel>(await _context.Students.FindAsync(id));
        }

        public async Task<bool> TryUpdateAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<StudentsViewModel> GetStudentByIDAsync(int? id)
        {
            return _mapper.Map<Students, StudentsViewModel>(await _context.Students.FirstOrDefaultAsync(s => s.StudentID == id));
        }

        public Students GetStudentByID(int? id)
        {
            return _context.Students.FirstOrDefault(s => s.StudentID == id);
        }

        public async Task<StudentsViewModel> DeleteConfirmedAsync(int id)
        {
            var students = await _context.Students.FindAsync(id);
            _context.Students.Remove(students);
            await _context.SaveChangesAsync();
            return _mapper.Map<Students, StudentsViewModel>(students);
        }
    }
}

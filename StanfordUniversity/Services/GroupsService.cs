using AutoMapper;
using StanfordUniversity.Data;
using StanfordUniversity.Models;
using StanfordUniversity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StanfordUniversity.Services
{
    public class GroupsService
    {
        private readonly SchoolContext _context;
        private readonly IMapper _mapper;

        public GroupsService(SchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<GroupsViewModel> GetIndex()
        {
            var groups = _context.Groups.ToList();
            return _mapper.Map<IEnumerable<Groups>, IEnumerable<GroupsViewModel>>(groups);
        }

        public async Task<GroupsViewModel> GetDetailsAsync(int? id)
        {
            var groups = await _context.Groups.Include(s => s.Students).FirstOrDefaultAsync(m => m.GroupID == id);
            return _mapper.Map<Groups, GroupsViewModel>(groups);
        }

        public async Task<GroupsViewModel> CreateAsync([Bind("Group_ID,Course_ID,Name")] Groups groups)
        {
            _context.Add(groups);
            await _context.SaveChangesAsync();
            return _mapper.Map<Groups, GroupsViewModel>(groups);
        }

        public async Task<GroupsViewModel> EditAsync(int? id)
        {
            return _mapper.Map<Groups, GroupsViewModel>(await _context.Groups.FindAsync(id));
        }

        public async Task<GroupsViewModel> EditAsync([Bind("Group_ID,Course_ID,Name")] Groups groups)
        {
            if (!GroupsExists(groups.GroupID))
                return null;

            try
            {
                _context.Update(groups);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return _mapper.Map<Groups, GroupsViewModel>(groups);
        }

        public async Task<GroupsViewModel> DeleteAsync(int? id)
        {
            var groups = await _context.Groups.FirstOrDefaultAsync(m => m.GroupID == id);
            return _mapper.Map<Groups, GroupsViewModel>(groups);
        }

        public async Task<GroupsViewModel> DeleteConfirmedAsync(int id)
        {
            Groups groups;

            if (IsGroupEmpty(id))
            {
                groups = await _context.Groups.FindAsync(id);
                _context.Groups.Remove(groups);
                await _context.SaveChangesAsync();
                groups = null;
            }
            else
            {
                groups = await _context.Groups.FirstOrDefaultAsync(m => m.GroupID == id);
            }
            return _mapper.Map<Groups, GroupsViewModel>(groups);
        }

        private bool GroupsExists(int id)
        {
            return _context.Groups.Any(e => e.GroupID == id);
        }

        public bool IsGroupEmpty(int id)
        {
            return !_context.Students.Any(e => e.GroupID == id);
        }
    }
}

using StanfordUniversity.Models;
using StanfordUniversity.Services;
using StanfordUniversity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace StanfordUniversity.Controllers
{
    public class GroupsController : Controller
    {
        private readonly GroupsService _servGroups;

        public GroupsController(GroupsService servGroups)
        {
            _servGroups = servGroups;
        }

        // GET: Groups
        public IActionResult Index()
        {
            return View(_servGroups.GetIndex());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var groups = await _servGroups.GetDetailsAsync(id);
            if (groups == null)
                return NotFound();

            return View(groups);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Group_ID,Course_ID,Name")] Groups groups)
        {
            if (ModelState.IsValid)
            {
                await _servGroups.CreateAsync(groups);
                return RedirectToAction(nameof(Index));
            }
            return View(groups);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> EditAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _servGroups.EditAsync(id);
            if (groups == null)
            {
                return NotFound();
            }
            return View(groups);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, Groups groups)
        {
            if (id != groups.GroupID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var groupsResult = await _servGroups.EditAsync(groups);
                if (groupsResult == null)
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(groups);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var groups = await _servGroups.DeleteAsync(id);
            if (groups == null)
                return NotFound();

            return View(groups);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groups = await _servGroups.DeleteConfirmedAsync(id);
            if (groups != null)
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. This group is not empty. Transfer students to a different group  " +
                    "and try again.";

                return View(groups);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

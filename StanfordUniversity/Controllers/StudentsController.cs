using StanfordUniversity.Models;
using StanfordUniversity.Services;
using StanfordUniversity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace StanfordUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentsService _servStudents;

        public StudentsController(StudentsService servStudents)
        {
            _servStudents = servStudents;
        }

        // GET: Students
        public IActionResult Index(
            string sortOrder,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["GroupSortParm"] = sortOrder == "Group" ? "Group_desc" : "Group";

            var students = _servStudents.GetSortStudents(sortOrder);

            int pageSize = 10;
            return View(PaginatedList<StudentsViewModel>.CreateAsync(students, pageNumber ?? 1, pageSize));
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _servStudents.DetailsAsync(id);
            if (students == null)
                return NotFound();

            return View(students);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Students students)
        {

            if (ModelState.IsValid)
            {
                var studentsResult = await _servStudents.CreateAsync(students);
                if (studentsResult == null)
                    return RedirectToAction(nameof(Index));
            }

            //Log the error (uncomment ex variable name and write a log.
            ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists " +
                "see your system administrator.");

            return View(students);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _servStudents.EditAsync(id);
            if (students == null)
            {
                return NotFound();
            }
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, [Bind("StudentID,Group_ID,FirstName,LastName")] StudentsViewModel students)
        {
            if (id != students.StudentID)
            {
                return NotFound();
            }

            var studentToUpdate = _servStudents.GetStudentByID(id);
            if (await TryUpdateModelAsync<Students>(studentToUpdate, "", s => s.FirstName, s => s.LastName, s => s.GroupID))
            {

                if (await _servStudents.TryUpdateAsync())
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _servStudents.GetStudentByIDAsync(id);
            if (students == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var students = await _servStudents.DeleteConfirmedAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

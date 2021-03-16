using StanfordUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace StanfordUniversity.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourcesService _servCource;

        public CoursesController(CourcesService servCource)
        {
            _servCource = servCource;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await _servCource.GetIndexAsync());
        }

        // GET: Courses/Details/5
        public IActionResult Details(int? id)
        {


            //var srv = new Mock<CourcesService>();
            //var coursesController = new CoursesController(_servCource);
            //var actionResult = coursesController.Index();
            //var viewResult = Assert.IsType<ViewResult>(actionResult);
            //Assert.IsAssignableFrom<Courses>(viewResult.ViewData.Model);



            if (id == null)
            {
                return NotFound();
            }

            var courses = _servCource.GetDetails(id);
            if (courses == null)
            {
                return NotFound();
            }

            return View(courses);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CourseID,Name,Description")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                _servCource.Create(courses);
                return RedirectToAction(nameof(Index));
            }
            return View(courses);
        }

        // GET: Courses/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courses = _servCource.Edit(id);
            if (courses == null)
            {
                return NotFound();
            }

            return View(courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CourseID,Name,Description")] Courses courses)
        {
            if (id != courses.CourseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var coursesResult = _servCource.Edit(courses);
                if (coursesResult == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            return View(courses);
        }
    }
}

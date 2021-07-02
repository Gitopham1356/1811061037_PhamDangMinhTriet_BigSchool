using _1811061037_PhamDangMinhTriet_BigSchool.Models;
using _1811061037_PhamDangMinhTriet_BigSchool.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace _1811061037_PhamDangMinhTriet_BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        
        public CoursesController() 
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Courses
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList()
            };
            return View(viewModel);
        }
    }
}
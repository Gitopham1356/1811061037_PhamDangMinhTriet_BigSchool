using _1811061037_PhamDangMinhTriet_BigSchool.Models;
using _1811061037_PhamDangMinhTriet_BigSchool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace _1811061037_PhamDangMinhTriet_BigSchool.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {

            var upcommingCourses = _dbContext.Courses
                                        .Include(c => c.Lecturer)
                                        .Include(c => c.Category)
                                        .Where(a => a.IsCanceled == false)
                                        .Where(c => c.Datetime > DateTime.Now);

            var userId = User.Identity.GetUserId();

            var viewModel = new CoursesViewModel
            {
                UpcommingCourses = upcommingCourses,
                ShowAction = User.Identity.IsAuthenticated,
                Followings = _dbContext.Followings.Where(f => userId != null && f.FolloweeId == userId).ToList(),
                Attendances = _dbContext.Attendances.Include(a => a.Course).ToList()

            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
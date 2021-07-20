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
        //hiển thị danh sách các khoá học sắp diễn ra có trong db(table course)
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
                ShowAction = User.Identity.IsAuthenticated             
            };

            return View(viewModel);
        }


    }
}

using _1811061037_PhamDangMinhTriet_BigSchool.Models;
using _1811061037_PhamDangMinhTriet_BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
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
        // hiển thị form tạo mới khoá học
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList(),
                Heading = "Add Course"
            };

            return View(viewModel);



        }
        // tạo mới khoá học sau đó direct về trang mine
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }

            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                Datetime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place
            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();

            return RedirectToAction("Mine", "Courses");
        }

        // hiển thị các khoá học đã tham gia 
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();


            var courses = _dbContext.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Course)
                .Include(l => l.Lecturer)
                .Include(c => c.Category)
                .ToList();

            var viewModel = new CoursesViewModel
            {
                UpcommingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated
            };

            return View(viewModel);

        }

        // hiển thị các giảng viên đã follow ( following.cshtml)
        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();

            var listLecturer = _dbContext.Followings
                .Where(a => a.FollowerId == userId)
                .Select(a => a.Followee);


            return View(listLecturer);
        }

        // hiển thị các khoá học do bản thân tạo ra ( mine.cshtml)
        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var mine = _dbContext.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Category)
                .Where(c => c.Datetime > DateTime.Now && c.LecturerId == userId).ToList();


            return View(mine);


        }
        // hiển thị form edit
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Courses.Single(c => c.Id == id && c.LecturerId == userId);

            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList(),
                Date = course.Datetime.ToString("dd/MM/yyyy"),
                Time = course.Datetime.ToString("HH:mm"),
                Place = course.Place,
                Heading = "Edit Course",
                Id = course.Id


            };

            return View("Create", viewModel);

        }

        // update thông tin khoá học
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseViewModel courseView)
        {
            if (!ModelState.IsValid)
            {
                courseView.Categories = _dbContext.Categories.ToList();
                return View("Create", courseView);
            }

            var userId = User.Identity.GetUserId();
            var course = _dbContext.Courses.Single(c => c.Id == courseView.Id && c.LecturerId == userId);

            course.Place = courseView.Place;
            course.Datetime = courseView.GetDateTime();
            course.CategoryId = courseView.Category;

            _dbContext.SaveChanges();

            return RedirectToAction("Mine", "Courses");


        }
    }
}

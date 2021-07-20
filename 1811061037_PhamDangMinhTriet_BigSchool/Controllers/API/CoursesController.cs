using _1811061037_PhamDangMinhTriet_BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _1811061037_PhamDangMinhTriet_BigSchool.Controllers.API
{
    public class CoursesController : ApiController
    {
        public ApplicationDbContext _dbContext { get; set; }

        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // xoá khoá học mà bản thân tạo ra( khai báo ở Mine.cshtml)
        [HttpDelete]
        public IHttpActionResult Cancel(int Id)
        {
            var userId = User.Identity.GetUserId();

            var course = _dbContext.Courses.Single(c => c.Id == Id && c.LecturerId == userId);

            if (course.IsCanceled)
            {
                return NotFound();
            }

            course.IsCanceled = true;
            _dbContext.SaveChanges();

            return Ok();

        }

    }
}

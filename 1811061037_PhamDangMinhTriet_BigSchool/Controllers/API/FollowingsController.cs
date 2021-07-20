using _1811061037_PhamDangMinhTriet_BigSchool.DTOs;
using _1811061037_PhamDangMinhTriet_BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace _1811061037_PhamDangMinhTriet_BigSchool.Controllers.API
{
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;

        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        //theo dõi các giảng viên 
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDTO)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDTO.FolloweeId))
                return BadRequest("Following already exists!");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDTO.FolloweeId
            };

            _dbContext.Followings.Add(following);
            _dbContext.SaveChanges();

            return Ok();


        }
        // huỷ theo dõi các giảng viên có trong lecturer i'm following
        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var follow = _dbContext.Followings
                .SingleOrDefault(a => a.FollowerId == userId && a.FolloweeId == id);

            if (follow == null)
                return NotFound();

            _dbContext.Followings.Remove(follow);
            _dbContext.SaveChanges();

            return Ok();
        }

    }
}

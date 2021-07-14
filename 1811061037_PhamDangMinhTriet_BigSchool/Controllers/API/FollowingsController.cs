using _1811061037_PhamDangMinhTriet_BigSchool.DTOs;
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
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _dbContext { get; set; }

        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }

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
        [HttpDelete]
        public IHttpActionResult UnFollow(string followerId)
        {
            var userId = User.Identity.GetUserId();

            var follow = _dbContext.Followings.Single(c => c.FolloweeId == followerId && c.FolloweeId == userId);

            _dbContext.Followings.Remove(follow);
            _dbContext.SaveChanges();
            return Ok();
        }

}
}

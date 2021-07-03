using _1811061037_PhamDangMinhTriet_BigSchool.DTOs;
using _1811061037_PhamDangMinhTriet_BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _1811061037_PhamDangMinhTriet_BigSchool.Controllers
{
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;

        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        /*
        [HttpPost]
        public IHttpActionResult Follow([FromBody] string FolloweeId)
        {
            var userId = User.Identity.GetUserId();

                if (_dbContext.Followings.Any(f => f.FolloweeId == userId && f.FolloweeId == FolloweeId))
                {
                    return BadRequest("Following already exists!");
                }
            


            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = FolloweeId
            };

            _dbContext.Followings.Add(following);
            _dbContext.SaveChanges();

            return Ok();
            //add = bbi5 loi
        }
        */
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDTO)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.Followings.Any(f => f.FolloweeId == userId && f.FolloweeId == followingDTO.FolloweeId))
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
    }
}

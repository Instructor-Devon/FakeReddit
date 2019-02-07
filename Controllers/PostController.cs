using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using FakeReddit.Models;

namespace FakeReddit.Controllers
{
    [Route("posts")]
    public class PostController : Controller
    {
        private int? UserId
        {
            get { return HttpContext.Session.GetInt32("userId"); }
        }
        private MainContext dbContext;
        public PostController(MainContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if(UserId == null)
                return RedirectToAction("Index", "Home");

            var test = dbContext.Votes.ToArray();
            
            ViewBag.UserId = UserId;
            ViewBag.AllCategories = dbContext.Categories.ToList();
            var model = dbContext.Posts
            // INNER JOIN on Posts.UserId = Users.UserId
                .Include(p => p.Author)
                .Include(p => p.Votes)
                .ToList();


            return View(model);
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            if(UserId == null)
                return RedirectToAction("Index", "Home");

            ViewBag.UserId = UserId;
            ViewBag.AllCategories = dbContext.Categories.ToList();
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(Post newPost)
        {
            if(ModelState.IsValid)
            {
                // create a post!
                dbContext.Posts.Add(newPost);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = UserId;
            ViewBag.AllCategories = dbContext.Categories.ToList();
            return View("New");
        }

        [HttpGet("delete/{postId}")]
        public IActionResult Delete(int postId)
        {
            // query for post to delete
            var postToDelete = dbContext.Posts.FirstOrDefault(p => p.PostId == postId);

            // additional checks...
            // is there even a post to delete?
            // is the UserId on post THE SAME as the user in session?
            if(postToDelete == null || postToDelete.UserId != UserId)
                return RedirectToAction("Index");

            dbContext.Posts.Remove(postToDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

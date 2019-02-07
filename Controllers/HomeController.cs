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
    public class HomeController : Controller
    {
        private MainContext dbContext;
        public HomeController(MainContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        // Create
        [HttpPost("create")]
        public IActionResult Create(LogRegModel model)
        {
            User user = model.NewUser;
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("NewUser.Email", "Email already in use");
                    return View("Index");
                }

                PasswordHasher<User> hasher = new PasswordHasher<User>();
                user.Password = hasher.HashPassword(user, user.Password);

                var newUser = dbContext.Users.Add(user).Entity;
                dbContext.SaveChanges();

                HttpContext.Session.SetInt32("userId", newUser.UserId);

                return RedirectToAction("Index", "Post");
            }
            
            return View("Index");
        }
        [HttpPost("login")]
        public IActionResult Login(LogRegModel model)
        {
            LoginUser user = model.Attempt;
            if(ModelState.IsValid)
            {
                User toLogin = dbContext.Users.FirstOrDefault(u => u.Email == user.EmailAttempt);
                if(toLogin == null)
                {
                    ModelState.AddModelError("Attempt.EmailAttempt", "Invalid Email/Password");
                    return View("Index");
                }
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(user, toLogin.Password, user.PasswordAttempt);
                if(result == PasswordVerificationResult.Failed)
                {
                    ModelState.AddModelError("Attempt.EmailAttempt", "Invalid Email/Password");
                    return View("Index");
                }
                // Log user into session
                HttpContext.Session.SetInt32("userId", toLogin.UserId);
                return RedirectToAction("Index", "Post");
            }
            return View("Index");
        }
        [HttpGet("logout")]
        public RedirectToActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        
        [HttpGet("{userId}")]
        public IActionResult Show(int userId)
        {
            var userModel = dbContext.Users
                // joins posts to user
                .Include(u => u.PublishedPosts)
                .ThenInclude(p => p.Category)
                // joins votes to user
                .Include(u => u.PostsVoted)
                // joins posts to votes
                .ThenInclude(v => v.VotedPost)
                .FirstOrDefault(u => u.UserId == userId);

            ViewBag.Upvoted = userModel.PostsVoted.Where(v => v.IsUpvote).Distinct();
            ViewBag.Downvoted = userModel.PostsVoted.Where(v => !v.IsUpvote);

            var selectCategoriesByCount = userModel.PublishedPosts
                .GroupBy(p => p.Category)
                .Select(g => new {Count = g.Count(), Category=g.Key});

            Category c = selectCategoriesByCount
                .Where(s => s.Count == selectCategoriesByCount.Max(sl => sl.Count))
                .Select(s => s.Category)
                .FirstOrDefault();
                
            if(userModel == null)
                return RedirectToAction("Index");

            return View(userModel);
        }
       
    }
}

using System.Linq;
using FakeReddit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeReddit.Controllers
{
    [Route("votes")]
    public class VoteController : Controller
    {
        private int? LoggedInUserId
        {
            get { return HttpContext.Session.GetInt32("userId"); }
        }
        private MainContext dbContext;
        public VoteController(MainContext context)
        {
            dbContext = context;
        }
        [HttpGet("{postId}/{isUpvote}")]
        public IActionResult Vote(int postId, bool isUpvote)
        {
            // make sure thres actually a user
            if(LoggedInUserId == null)
                return RedirectToAction("Index", "Home");

            // has this user voted yet?

            Vote existingVote = dbContext.Votes
                .FirstOrDefault(v => v.UserId == LoggedInUserId && v.PostId == postId);

            if(existingVote != null)
            {
                // if new vote attempt is oppisite? switch the IsUpvote
                if(existingVote.IsUpvote == !isUpvote)
                {
                    existingVote.IsUpvote = !existingVote.IsUpvote;
                }
                else
                // otherwise delete it
                    dbContext.Votes.Remove(existingVote);
            }
            
            else
            {

                Vote newVote = new Vote()
                {
                    PostId = postId,
                    IsUpvote = isUpvote,
                    UserId = (int)LoggedInUserId
                };

                dbContext.Votes.Add(newVote);

            }
            dbContext.SaveChanges();

            return RedirectToAction("Index", "Post");
        }       
    }
}
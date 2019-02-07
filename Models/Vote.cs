namespace FakeReddit.Models
{
    public class Vote
    {
        public int VoteId {get;set;}
        public bool IsUpvote {get;set;}
        public int UserId {get;set;}
        public int PostId {get;set;}
        // Navigation Properties
        public User Voter {get;set;}
        public Post VotedPost {get;set;}
    }
}
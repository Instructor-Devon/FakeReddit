using System.Collections.Generic;

namespace FakeReddit.Models
{
    public class LogRegModel
    {
        public User NewUser {get;set;}
        public LoginUser Attempt {get;set;}
    }
}
using System.Collections.Generic;

namespace FakeReddit.Models
{
    public class Category
    {
        public int CategoryId {get;set;}
        public string Title {get;set;}
        public List<Post> Posts {get;set;}
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FakeReddit.Models
{
    public class Post
    {
        [Key]
        public int PostId {get;set;}
        [Required]
        public string Title {get;set;}
        [Required]
        public string Content {get;set;}
        public int UserId {get;set;}
        [Display(Name="Category")]
        public int CategoryId {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;

        // Navigation Properties
        public User Author {get;set;}
        public Category Category {get;set;}
        public List<Vote> Votes {get;set;}

    }
}
using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models
{
    public partial class Profiles
    {
        public Profiles()
        {
            Assets = new HashSet<Assets>();
            BlogPosts = new HashSet<BlogPosts>();
        }

        public int Id { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorName { get; set; }
        public string Avatar { get; set; }
        public string Bio { get; set; }
        public string BlogTheme { get; set; }
        public string Description { get; set; }
        public string IdentityName { get; set; }
        public string Image { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Logo { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }

        public ICollection<Assets> Assets { get; set; }
        public ICollection<BlogPosts> BlogPosts { get; set; }
    }
}

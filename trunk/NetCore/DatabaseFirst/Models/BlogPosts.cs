using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models
{
    public partial class BlogPosts
    {
        public BlogPosts()
        {
            PostCategories = new HashSet<PostCategories>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime LastUpdated { get; set; }
        public int PostViews { get; set; }
        public int ProfileId { get; set; }
        public DateTime Published { get; set; }
        public float Rating { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }

        public Profiles Profile { get; set; }
        public ICollection<PostCategories> PostCategories { get; set; }
    }
}

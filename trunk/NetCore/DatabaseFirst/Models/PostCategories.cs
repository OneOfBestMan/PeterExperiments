using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models
{
    public partial class PostCategories
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public int CategoryId { get; set; }
        public DateTime LastUpdated { get; set; }

        public BlogPosts BlogPost { get; set; }
        public Categories Category { get; set; }
    }
}

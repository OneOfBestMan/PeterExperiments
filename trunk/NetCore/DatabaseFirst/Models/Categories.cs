using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models
{
    public partial class Categories
    {
        public Categories()
        {
            PostCategories = new HashSet<PostCategories>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string ImgSrc { get; set; }
        public DateTime LastUpdated { get; set; }
        public int ParentId { get; set; }
        public int ProfileId { get; set; }
        public int Rank { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }

        public ICollection<PostCategories> PostCategories { get; set; }
    }
}

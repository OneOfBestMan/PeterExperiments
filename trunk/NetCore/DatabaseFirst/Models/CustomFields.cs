using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models
{
    public partial class CustomFields
    {
        public int Id { get; set; }
        public string CustomKey { get; set; }
        public int CustomType { get; set; }
        public string CustomValue { get; set; }
        public DateTime LastUpdated { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
    }
}

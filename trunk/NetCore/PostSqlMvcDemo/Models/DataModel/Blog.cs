using System;

namespace NetCoreTest.Models.DataModel
{
    public class Blog:BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

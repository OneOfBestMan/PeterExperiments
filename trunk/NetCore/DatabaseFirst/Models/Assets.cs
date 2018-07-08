using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models
{
    public partial class Assets
    {
        public int Id { get; set; }
        public int AssetType { get; set; }
        public int DownloadCount { get; set; }
        public DateTime LastUpdated { get; set; }
        public long Length { get; set; }
        public string Path { get; set; }
        public int ProfileId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public Profiles Profile { get; set; }
    }
}

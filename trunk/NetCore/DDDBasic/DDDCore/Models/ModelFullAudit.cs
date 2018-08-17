using System;
using System.Collections.Generic;
using System.Text;

namespace LC.SDK.Core.Models
{
    public class ModelFullAudit<T> : ModelBase<T>
    {
        public DateTime? LastModifiedTime { get; set; }

        public DateTime? DeletionTime { get; set; }

        public string CreatorId { get; set; }

        public string DeletorId { get; set; }

        public bool IsDeleted { get; set; }
    }
}

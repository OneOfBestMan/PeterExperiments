using System;
using System.ComponentModel.DataAnnotations;

namespace LC.SDK.Core.Models
{
    public class ModelBase<T>
    {
        [Key]
        public T Id { get; set; }
        public DateTime CreationTime { get; set; }
        
    }
}

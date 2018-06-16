using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTest.Models.DataModel
{
    public class User: BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public string NickName { get; set; }
        public DateTime CreationTime { get; set; }

    }
}

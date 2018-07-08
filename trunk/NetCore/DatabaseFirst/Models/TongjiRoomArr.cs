using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models
{
    public partial class TongjiRoomArr
    {
        public int Id { get; set; }
        public int WeekDay { get; set; }
        public string RoomNum { get; set; }
        public string Arrangement { get; set; }
    }
}

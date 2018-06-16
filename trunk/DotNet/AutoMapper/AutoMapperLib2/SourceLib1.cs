using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapperLib2
{
   public class SourceLib2
    {
        public string SourceValue { get; set; }
        public string OtherValue { get; set; }
    }

    public class DestinationLib2
    {
        public string SourceValue { get; set; }

        public string OtherNS { get; set; }

        public DestinationInner2 Inner { get; set; }

    }

    public class DestinationInner2
    {
        public string OtherValue { get; set; }
    }

    public class Lib2Profile : Profile
    {
        public Lib2Profile()
        {
            CreateMap<SourceLib2, DestinationLib2>();
        }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapperLib1
{
    public class SourceLib1
    {
        public string SourceValue { get; set; }
        public string OtherValue { get; set; }
        public DestinationInner1 Inner { get; set; }
    }

    public class DestinationLib1
    {
        public string SourceValue { get; set; }

        public string OtherNS { get; set; }
        public DestinationInner1 Inner { get; set; }
        public NotExistDestination NotExsitClass { get; set; }

    }

    public class DestinationInner1
    {
        public string OtherValue { get; set; }
    }
    public class NotExistDestination
    {
        public string OtherValue { get; set; }
    }
    public class Lib1Profile : Profile
    {
        public Lib1Profile()
        {
            CreateMap<SourceLib1, DestinationLib1>();
        }
    }
}

using AutoMapper;
using AutoMapperLib1;
using AutoMapperLib2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<IMapperConfigurationExpression> action = cfg =>
             {
                 cfg.AddProfile<Lib1Profile>();
                 cfg.AddProfile<Lib2Profile>();
             };
            var config = new MapperConfiguration(action);
            Mapper.Initialize(action
            );

            var source1 = new SourceLib1() { SourceValue = "s1", OtherValue = "haha",Inner=new DestinationInner1() { OtherValue="hahahha"} };
            var source2 = new SourceLib2() { SourceValue = "s2", OtherValue = "hehe" };
            var dest1 = config.CreateMapper().Map<SourceLib1, DestinationLib1>(source1);
            var dest2 = config.CreateMapper().Map<SourceLib2, DestinationLib2>(source2);

            Console.WriteLine(dest1.SourceValue + " " + dest1.OtherNS);
            Console.WriteLine(dest2.SourceValue + " " + dest2.OtherNS);
            Console.WriteLine(dest1.Inner == null);
            var dest11 = Mapper.Map<SourceLib1, DestinationLib1>(source1);
            var dest22 = Mapper.Map<SourceLib2, DestinationLib2>(source2);

            Console.WriteLine(dest11.SourceValue + " " + dest11.OtherNS);
            Console.WriteLine(dest11.Inner==null);
            Console.WriteLine(dest22.SourceValue + " " + dest22.OtherNS);



            Console.ReadKey();
        }
    }
}

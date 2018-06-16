using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CastleAdditionalInterfaces
{
    public interface IFooService
    {
        void Do();
    }
    public class FooService:IFooService
    {
        public void Do()
        {
            Console.WriteLine("How are you doing ?");
        }
    }

    public class FooController
    { 
    
    }
}

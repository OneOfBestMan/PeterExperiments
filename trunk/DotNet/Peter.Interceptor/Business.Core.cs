using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Core
{
    public interface IBllGetOrder
    {
        string Get(string str);
    }
    //public interface IBllDealOrder
    //{
    //    IBllGetOrder bllGetOrder { get; set; }
    //    string Deal(string str);

    //}
    public class bllGetOrder1 : IBllGetOrder
    {

        public virtual string Get(string str)
        {
            return str + "_" + "something1";
        }
    }
    public class bllGetOrder2 : IBllGetOrder
    {

        public virtual string Get(string str)
        {
            return str + "+" + "something2";
        }
    }
    //public class MyOrder : IBllDealOrder
    //{
    //    public IBllGetOrder bllGetOrder
    //    {
    //        get;
    //        set;
    //    }
    //    public virtual string Deal(string str)
    //    {
    //        return bllGetOrder.Get(str);
    //    }
    //}
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace LC.SDK.Utiles
{



    //Define this extension method (which is reusable):

    public static class HashCodeByPropertyExtensions
    {
        public static int GetHashCodeOnProperties<T>(this T inspect)
        {
            return inspect.GetType().GetProperties().Select(o => o.GetValue(inspect)).GetListHashCode();
        }

        public static int GetListHashCode<T>(this IEnumerable<T> sequence)
        {
            return sequence
                .Select(item => item.GetHashCode())
                .Aggregate((total, nextCode) => total ^ nextCode);
        }
    }


}

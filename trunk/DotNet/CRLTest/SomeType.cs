using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRLTest
{
    internal sealed class SomeType
    {
        private int m_x = 5;
        private string m_s = "Hi there";
        private double m_d = 3.1415;
        private Byte m_b;
        public SomeType() { }
        public SomeType(int x) { }
        public SomeType(string s)
        {
            m_d = 10;
        }
    }

    internal struct Point
    {
        public int m_z ;
        public int m_x, m_y;
        //public Point()
        //{
        //    m_x = m_y = 5;
        //}
    }
}

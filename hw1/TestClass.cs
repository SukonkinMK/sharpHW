using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw1
{
    internal class TestClass
    {
        [CustomName("CustomFieldName1")]
        public int I { get; set; }
        [CustomName("CustomFieldName2")]
        public int II { get; set; }
        [CustomName("CustomFieldName3")]
        public string S { get; set; }
        [CustomName("CustomFieldName4")]
        public decimal D { get; set; }
        public char[] C { get; set; }
        private char[] CC { get; set; }

        public TestClass()
        {

        }
        public TestClass(int i)
        {
            I = i;
        }
        public TestClass(int i, int ii, string s, decimal d, char[] c, char[] cc) : this(i)
        {
            S = s;
            D = d;
            C = c;
            II = ii;
            CC = cc;
        }

    }
}

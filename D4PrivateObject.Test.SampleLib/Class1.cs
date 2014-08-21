using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4PrivateObject.Test.SampleLib
{
    /// <summary>
    /// for test sample class
    /// </summary>
    public class TestSample
    {
        private int privateInt = 10;
        private string privateStr = "masuda";

        private int _privateProp = 20;
        private int privateProp
        {
            get { return _privateProp; }
        }

        private int privateMethod(int x, int y)
        {
            return x + y;
        }

        public void init( int a, string b )
        {
            privateInt = a;
            privateStr = b;
        }
        public int GetInt() { return privateInt; }
        public string GetStr() { return privateStr; }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using D4Reflection.Lib;
using D4PrivateObject.Test.SampleLib;

namespace D4PrivateObject.Test
{
    [TestClass]
    public class PrivateObjectTest
    {
        [TestMethod]
        public void TestGetField()
        {
            var target = new TestSample();
            var o = new D4Reflection.Lib.PrivateObject(target);

            target.init(1, "tomoaki");
            Assert.AreEqual(1, o.GetField<int>("privateInt"));
            Assert.AreEqual("tomoaki", o.GetField<string>("privateStr"));
            target.init(10, "masuda");
            Assert.AreEqual(10, o.GetField<int>("privateInt"));
            Assert.AreEqual("masuda", o.GetField<string>("privateStr"));
        }

        [TestMethod]
        public void TestSetField()
        {
            var target = new TestSample();
            var o = new D4Reflection.Lib.PrivateObject(target);

            target.init(1, "tomoaki");
            Assert.AreEqual(1, o.GetField<int>("privateInt"));
            Assert.AreEqual("tomoaki", o.GetField<string>("privateStr"));

            o.SetField("privateInt", 20);
            o.SetField("privateStr", "Japan");
            
            Assert.AreEqual(20, o.GetField<int>("privateInt"));
            Assert.AreEqual("Japan", o.GetField<string>("privateStr"));
        }

        [TestMethod]
        public void TestGetProperty()
        {
            var target = new TestSample();
            var o = new D4Reflection.Lib.PrivateObject(target);

            Assert.AreEqual(20, o.GetProperty<int>("privateProp"));

            o.SetField("_privateProp", 100 );
            Assert.AreEqual(100, o.GetProperty<int>("privateProp"));
        }

        [TestMethod]
        public void TestMethod()
        {
            var target = new TestSample();
            var o = new D4Reflection.Lib.PrivateObject(target);

            Assert.AreEqual(30, o.Invoke<int>("privateMethod", new object[] { 10, 20 }));
            Assert.AreEqual(40, o.Invoke<int>("privateMethod", new object[] { 20, 20 }));

        }
    }
}

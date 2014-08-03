using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using D4Reflection.Lib;
using D4Reflection.Lib.Extentions;  // for TypeExtentions

namespace D4Reflection.Test
{
    [TestClass]
    public class TestType
    {
        [TestMethod]
        public void TestCreateInstance()
        {
            var t = typeof(ClassA);
            var a = t.CreateInstance<ClassA>();
            Assert.IsNotNull(a);
            Assert.AreEqual(0, a.PropA);
            Assert.AreEqual(0, a.FindField<int>("propC", true));
            Assert.AreEqual(null, a.FindField<string>("propD", true));

            a = t.CreateInstance<ClassA>(100, "start");
            Assert.IsNotNull(a);
            Assert.AreEqual(0, a.PropA);
            Assert.AreEqual(100, a.FindField<int>("propC", true));
            Assert.AreEqual("start", a.FindField<string>("propD", true));

            /// コンストラクタで引数間違い
            a = t.CreateInstance<ClassA>(100, 999);
            Assert.AreEqual(null, a);
            
        }
    }
}

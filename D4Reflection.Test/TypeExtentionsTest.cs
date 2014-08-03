using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using D4Reflection.Lib;
using D4Reflection.Lib.Extentions;  // for TypeExtentions
using System.Reflection;
using System.Linq;

namespace D4Reflection.Test
{

    public class ClassA
    {
        /// <summary>
        /// 公開プロパティ
        /// </summary>
        public int PropA { get; set; }
        public string PropB { get; set; }

        /// <summary>
        /// 未公開プロパティ
        /// XAMLのコードビハイドはこっちのほう
        /// </summary>
        private int propC;
        private string propD;

        public int GetPropC { get { return propC; } }
        public string GetProcD { get { return propD;  } }

        public ClassA() { }
        public ClassA(int c, string d)
        {
            propC = c;
            propD = d;
        }

        public void FuncA() { PropA = 999; }
        public void FuncA(int a) { this.PropA = a;  }

        public string FuncB(int x, int y) { return "ans." + (x + y).ToString(); }
        private string FuncC(int x, int y) { return "private." + (x + y).ToString(); }


    }


    /// <summary>
    /// リフレクション拡張のテスト
    /// </summary>
    [TestClass]
    public class TypeExtentionsTest
    {
        [TestMethod]
        public void TestPublicReflection()
        {
            var target = new ClassA(30, "tomoaki") { PropA = 10, PropB = "masuda" };

            int a = target.FindPropery<int>("PropA");
            Assert.AreEqual(10, a);

            string b = target.FindPropery<string>("PropB");
            Assert.AreEqual("masuda", b);

            // 未知プロパティの場合は null を返す
            object o = target.FindPropery<object>("ErrorProp");
            Assert.AreEqual(null, o);

            // プライベートプロパティは探索しない
            // 戻り値は default(T) = 0
            int c = target.FindPropery<int>("propC");
            Assert.AreEqual(0, c);
        }

        [TestMethod]
        public void TestPrivateReflection()
        {
            var target = new ClassA(30, "tomoaki") { PropA = 10, PropB = "masuda" };

            // プライベートプロパティは探索しない
            // 戻り値は default(T) = 0
            int c = target.FindField<int>("propC");
            Assert.AreEqual(0, c);

#if false
            Type t = target.GetType();
            /*
            var bp = System.Reflection.BindingFlags.GetField |  // 1024
                    System.Reflection.BindingFlags.Instance | // 4 
                    System.Reflection.BindingFlags.NonPublic; // 32 
            */
            // var nm = typeof(System.Reflection.BindingFlags).AssemblyQualifiedName;
            var bt = Type.GetType("System.Reflection.BindingFlags, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            object vp = Convert.ChangeType(Enum.Parse(bt, "GetField,Instance,NonPublic"), bt);

                Type tt = t.GetType();
                var mi = tt.GetRuntimeMethod("GetField", new Type[] { typeof(string), bt });
                var fi0 = mi.Invoke(t, new object[] { "propC", vp });
                var fi = fi0 as FieldInfo;

            // var fi = t.GetField("propC", bp);
            var obj = fi.GetValue(target);
#endif

            // プライベートプロパティを探索する
            int cc = target.FindField<int>("prpoC", true);
            Assert.AreEqual(30, cc);
        }

        /// <summary>
        /// プロパティとフィールドを同時に探索する
        /// </summary>
        [TestMethod]
        public void TestTargetReflection()
        {
            var target = new ClassA(30, "tomoaki") { PropA = 10, PropB = "masuda" };



            // プライベートプロパティは探索しない
            // 戻り値は default(T) = 0
            int a = target.FindField<int>("PropA");
            Assert.AreEqual(10, a);

            // プライベートプロパティを探索する
            int c = target.FindField<int>("propC", true);
            Assert.AreEqual(30, c);
        }

        [TestMethod]
        public void TestFuncReflection()
        {
            var target = new ClassA(30, "tomoaki") { PropA = 10, PropB = "masuda" };

            // 戻り値が void 
            target.FindMethod<object>("FuncA");
            Assert.AreEqual(999, target.PropA);

            // 戻り値が void 
            target.FindMethod<object>("FuncA", 100 );
            Assert.AreEqual(100, target.PropA);

            // 戻り値が string
            var ans = target.FindMethod<string>("FuncB", 10, 20);
            Assert.AreEqual("ans.30", ans);

        }

        [TestMethod]
        public void TestFuncPrivateReflection()
        {
            var target = new ClassA(30, "tomoaki") { PropA = 10, PropB = "masuda" };

            // プライベートは探索しない
            var ans = target.FindMethod<string>("FuncC", 10, 20);
            Assert.AreEqual(null, ans);

            // プライベートを探索する
            ans = target.FindMethodPrivate<string>("FuncC", 10, 20);
            Assert.AreEqual("private.30", ans);
        }
    }
}

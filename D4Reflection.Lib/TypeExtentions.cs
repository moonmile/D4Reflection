using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.InteropServices;

namespace D4Reflection.Lib.Extentions
{
    public static class TypeExtentions
    {
        /// <summary>
        /// 指定オブジェクトのプロパティをリフレクションで取得する
        /// プライベートプロパティの探索も可能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <param name="searchPrivate"></param>
        /// <returns></returns>
        public static T FindPropery<T>( this object target, string name, bool searchPrivate = false ) {
            Type t = target.GetType();
            var pi = t.GetRuntimeProperty(name);
            if ( pi == null && searchPrivate == true ) {
                pi = t.GetTypeInfo().GetDeclaredProperty(name);
            }
            if (pi != null)
            {
                var obj = pi.GetValue(target);
                if (obj != null)
                {
                    return (T)obj;
                }
            }
            return default(T);
        }

        /// <summary>
        /// 指定オブジェクトのプロパティをリフレクションで取得する
        /// プライベートプロパティの探索も可能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T FindField<T>(this object target, string name, bool searchPrivate = false )
        {
            Type t = target.GetType();
            var pi = t.GetRuntimeField(name);
            if (pi == null && searchPrivate == true)
            {
                // MSDN ではパブリックフィールドになっているが、プライベートも取得できる
                pi = t.GetTypeInfo().GetDeclaredField(name);
                /*  // runtime error
                    var bt = Type.GetType("System.Reflection.BindingFlags, mscorlib"); 
                    object vp = Convert.ChangeType(Enum.Parse(bt, "GetField,Instance,NonPublic"), bt);
                    var mi = t.GetType().GetRuntimeMethod("GetField", new Type[] { typeof(string), bt });
                    var pi = mi.Invoke(t, new object[] { name, vp }) as FieldInfo;
                 */
                /* // not found Getfield method.
                var bt = Type.GetType("System.Reflection.BindingFlags, mscorlib");
                object vp = Convert.ChangeType(Enum.Parse(bt, "GetField,Instance,NonPublic"), bt);
                dynamic tx = target.GetType();
                object pi0 = tx.GetField(name, vp);
                pi = pi0 as FieldInfo;
                */
            
            }
            if (pi != null)
            {
                var obj = pi.GetValue(target);
                if (obj != null)
                {
                    return (T)obj;
                }
            }
            return default(T);
        }
        /// <summary>
        /// プロパティとフィールドを同時に探索する
        /// MVVM バインド用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T FindTarget<T>(object target, string name, bool searchPrivate = false )
        {
            var obj = target.FindPropery<object>(name, searchPrivate);
            if ( obj == null ) {
                obj = target.FindField<object>(name, searchPrivate);
                if ( obj == null ) {
                    return default(T);
                }
            }
            return (T)obj;
        }

        /// <summary>
        /// 指定オブジェクトのメソッドをリフレクションで実行する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T FindMethod<T>(this object target, string name )
        {
            Type t = target.GetType();
            var mi = t.GetRuntimeMethod(name, new Type[] { });
            /*
            if (mi == null && searchPrivate == true)
            {
                mi = t.GetTypeInfo().GetDeclaredMethod(name);
            }
            */
            if (mi != null)
            {
                var obj = mi.Invoke(target, new object[] { });
                if (obj != null)
                {
                    return (T)obj;
                }
            }
            return default(T);
        }

        /// <summary>
        /// 指定オブジェクトのメソッドをリフレクションで実行する
        /// 引数付き
        /// 注意:省略可能な(optionalな）引数も指定する必要がある。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T FindMethod<T>(this object target, string name, params object[] plst)
        {
            Type t = target.GetType();
            var tlst = new List<Type>();
            var olst = new List<object>();
            foreach (var it in plst)
            {
                tlst.Add(it.GetType());
                olst.Add(it);
            }
            var mi = t.GetRuntimeMethod(name, tlst.ToArray());
            if (mi != null)
            {
                var obj = mi.Invoke(target, olst.ToArray());
                if (obj != null)
                {
                    return (T)obj;
                }
            }
            return default(T);
        }
        /// <summary>
        /// 指定オブジェクトのメソッドをリフレクションで実行する
        /// プライベートメソッドも探索に含める
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T FindMethodPrivate<T>(this object target, string name, params object[] plst)
        {
            Type t = target.GetType();
            var tlst = new List<Type>();
            var olst = new List<object>();
            foreach (var it in plst)
            {
                tlst.Add(it.GetType());
                olst.Add(it);
            }
            var mi = t.GetRuntimeMethod(name, tlst.ToArray());
            if (mi == null)
            {
                mi = t.GetTypeInfo().GetDeclaredMethod(name);
            }
            if (mi != null)
            {
                var obj = mi.Invoke(target, olst.ToArray());
                if (obj != null)
                {
                    return (T)obj;
                }
            }
            return default(T);
        }

        /// <summary>
        /// インスタンスを作成
        /// </summary>
        /// <param name="t"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T CreateInstance<T>(this Type t , params object[] parameters) where T : class
        {
            try
            {
                return (T)System.Activator.CreateInstance(typeof(T), parameters);
            }
            catch
            {
                // パラメータ間違いの時は null を返す
                return null;
            }
        }
        /// <summary>
        /// キャスト先を指定せずにインスタンスを作成する
        /// Type から直接インスタンスを作成するときに使う
        /// </summary>
        /// <param name="t"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object CreateInstance(this Type t, params object[] parameters)
        {
            try
            {
                return System.Activator.CreateInstance(t, parameters);
            }
            catch
            {
                // パラメータ間違いの時は null を返す
                return null; 
            }
        }

        /// <summary>
        /// キャストメソッド
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T Cast<T>(this object target)
        {
            return (T)Convert.ChangeType(target, typeof(T));
        }

        /// <summary>
        /// キャスト先の型を指定せずにキャストする
        /// Reflection の SetValue で型指定を合わせるために使う
        /// </summary>
        /// <param name="target"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object Cast(this object target, Type t )
        {
            return Convert.ChangeType(target, t);
        }
    }
}

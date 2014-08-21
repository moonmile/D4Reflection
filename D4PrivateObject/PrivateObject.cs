using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace D4Reflection.Lib
{
    public class PrivateObject
    {
        public object Target { get; set; }
        public Type RealType { get; set; }
        public PrivateObject(object target)
        {
            this.Target = target;
            this.RealType = target.GetType();
        }

        public object GetField(string name)
        {
            Type t = this.RealType;
            var pi = t.GetRuntimeField(name);
            if (pi == null)
            {
                // MSDN ではパブリックフィールドになっているが、プライベートも取得できる
                pi = t.GetTypeInfo().GetDeclaredField(name);
            }
            if (pi != null)
            {
                var obj = pi.GetValue(this.Target);
                if (obj != null)
                {
                    return obj;
                }
            }
            return null;
        }
        public T GetField<T>(string name)
        {
            object o = GetField(name);
            return o == null ? default(T) : (T)o;
        }

        public void SetField(string name, object value)
        {
            Type t = this.RealType;
            var pi = t.GetRuntimeField(name);
            if (pi == null)
            {
                pi = t.GetTypeInfo().GetDeclaredField(name);
            }
            if (pi != null)
            {
                pi.SetValue(this.Target, Convert.ChangeType(value, pi.FieldType));
            }
        }


        public object GetProperty(string name, params object[] args)
        {
            Type t = this.RealType;
            var pi = t.GetRuntimeProperty(name);
            if (pi == null)
            {
                pi = t.GetTypeInfo().GetDeclaredProperty(name);
            }
            if (pi != null)
            {
                object obj = pi.GetValue(this.Target, args);
                if (obj != null)
                {
                    return obj;
                }
            }
            return null;
        }
        public T GetProperty<T>(string name, params object[] args)
        {
            object o = GetProperty(name, args);
            return o == null ? default(T) : (T)o;
        }

        public void SetProperty(string name, object value, params object[] args)
        {
            Type t = this.RealType;
            var pi = t.GetRuntimeProperty(name);
            if (pi == null)
            {
                pi = t.GetTypeInfo().GetDeclaredProperty(name);
            }
            if (pi != null)
            {
                pi.SetValue(this.Target, Convert.ChangeType(value, pi.PropertyType), args);
            }
        }

        public object Invoke(string name, params object[] args)
        {
            Type t = this.RealType;
            var lst = new List<Type>();
            foreach ( var it in args ) 
                lst.Add( it.GetType() );
            var mi = t.GetRuntimeMethod(name, lst.ToArray());
            if (mi == null)
            {
                mi = t.GetTypeInfo().GetDeclaredMethod(name);
            }
            if (mi != null)
            {
                return mi.Invoke(this.Target, args);
            }
            return null;
        }
        public T Invoke<T>(string name, params object[] args)
        {
            var o = Invoke(name, args);
            return o == null ? default(T) : (T)o;
        }
    }
}

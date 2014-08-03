using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using D4Reflection.Lib.Extentions;

namespace D4Reflection.Lib
{

    public class BindObject
    {
        /// <summary>
        /// バインド先のオブジェクト
        /// </summary>
        public dynamic Target { get; set; }

        /// <summary>
        /// ターゲットを指定してバインドオブジェクトを作成
        /// </summary>
        /// <param name="target"></param>
        public BindObject(object target)
        {
            this.Target = target;
        }

        public object FindName(string name)
        {
            return TypeExtentions.FindTarget<object>(this.Target, name, true);
        }
        public T FindName<T>(string name)
        {
            return TypeExtentions.FindTarget<T>(this.Target, name, true);
        }


        public void AddEventHandler(string eventName, Action<object, object> hdr)
        {
            object _target = this.Target;
            if (_target != null)
            {
                var ei = _target.GetType().GetRuntimeEvent(eventName);
                if (ei != null)
                {
                    var dt = ei.AddMethod.GetParameters()[0].ParameterType;

                    var handler =
                        new Action<object, object>(
                            (sender, eventArgs) =>
                            {
                                // hdr.DynamicInvoke(sender, eventArgs);
                                // hdr.DynamicInvoke(sender, new EventArgs());
                                hdr.Invoke(sender, new EventArgs());
                            });
                    var handlerInvoke = handler.GetType().GetRuntimeMethod("Invoke",
                         new Type[] { typeof(object), typeof(Type[]) });
                    var dele = handlerInvoke.CreateDelegate(dt, handler);

                    var add = new Func<Delegate, EventRegistrationToken>(
                        (t) =>
                        {
                            var para = ei.AddMethod.GetParameters();
                            var ret = ei.AddMethod.Invoke(_target, new object[] { t });
                            if (ret != null)
                            {
                                return (EventRegistrationToken)ret;
                            }
                            else
                            {
                                // dummy for Xamarin.Forms
                                return new EventRegistrationToken();
                            }
                        });
                    var remove = new Action<EventRegistrationToken>(
                        (t) =>
                        {
                            ei.RemoveMethod.Invoke(_target, new object[] { t });
                        });
                    try
                    {
                        WindowsRuntimeMarshal.AddEventHandler<Delegate>(add, remove, dele);
                    }
                    catch 
                    {
                        // MonoDroid は WindowsRuntimeMarshal.AddEventHandler が未実装なのでこちらで、
                        // 追加のみ
                        add(dele);
                    }
                }
            }
        }
    }
}

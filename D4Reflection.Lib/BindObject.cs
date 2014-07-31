using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;

namespace D4Reflection.Lib
{
    public interface IImageSource { }
    public interface IBitmapSource : IImageSource { }
    public interface IBitmapImage : IBitmapSource { }
    public interface IImage
    {
        IImageSource Source { get; set; }
    }
    public interface ITextBlock
    {
        string Text { get; set; }
    }
    public class BindObject
    {
        public dynamic Target { get; set; }
        public BindObject(object target)
        {
            this.Target = target;
        }
        public object CreateInstance(Type t, params object[] parameters)
        {
            return System.Activator.CreateInstance(t, parameters);
        }
        public object FindName(string propName)
        {
            // var mi = this.Target.GetType().GetRuntimeMethod("FindName", new Type[] { typeof(string) });
            // return mi.Invoke(this.Target, new object[] { propName });
            return this.Target.FindName(propName);
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
                                hdr.DynamicInvoke(sender, new EventArgs());
                            });
                    var handlerInvoke = handler.GetType().GetRuntimeMethod("Invoke",
                         new Type[] { typeof(object), typeof(Type[]) });
                    var dele = handlerInvoke.CreateDelegate(dt, handler);

                    var add = new Func<Delegate, EventRegistrationToken>(
                        (t) =>
                        {
                            var ret = ei.AddMethod.Invoke(_target, new object[] { t });
                            return (EventRegistrationToken)ret;
                        });
                    var remove = new Action<EventRegistrationToken>(
                        (t) =>
                        {
                            ei.RemoveMethod.Invoke(_target, new object[] { t });
                        });
                    WindowsRuntimeMarshal.AddEventHandler<Delegate>(add, remove, dele);
                }
            }
        }
    }
    public class TextBlock : BindObject, ITextBlock
    {
        public TextBlock(object target) : base(target) { }
        public string Text
        {
            get { return this.Target.Text; }
            set { this.Target.Text = value; }
        }
    }

    public class Button : BindObject
    {
        public Button(object target)
            : base(target)
        {
            // dynamic には、Click イベントにラムダ式が設定できない
            // this.Target.Click += (s,e) => { this.Click.Invoke( this, new EventArgs());}

            /// リフレクションを使って設定する
            /// AddEventHandler の中身は dynamic を使ってもいいかもしれない
            this.AddEventHandler("Click", (s, e) => this.Click.Invoke(this, e as EventArgs));
        }
        public string Content
        {
            get { return this.Target.Content; }
            set { this.Target.Content = value; }
        }
        public event EventHandler<EventArgs> Click;
    }
    public class ImageSource : BindObject, IImageSource
    {
        public ImageSource(object target) : base(target) { }
    }
    public class BitmapSoruce : BindObject, IBitmapSource
    {
        public BitmapSoruce(object target) : base(target) { }
    }

    public class BitmapImage : BindObject, IBitmapImage
    {
        public BitmapImage(Uri uri)
            : base(null)
        {
            // こうすると動的ロードできる
            var t = Type.GetType("Windows.UI.Xaml.Media.Imaging.BitmapImage, Windows.UI.Xaml, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime");
            this.Target = base.CreateInstance(t, uri);
        }
    }

    public class Image : BindObject, IImage
    {
        public Image(object target) : base(target) { }
        /// <summary>
        /// ラップしたクラス同士のプロパティ取得/設定
        /// </summary>
        public IImageSource Source
        {
            get { return this.Target.Source; }
            set
            {
                dynamic o = value;
                this.Target.Source = o.Target;
            }
        }
    }

#if false
    public class MainPage
    {
        protected object _target;
        public dynamic Target
        {
            get { return _target; }
            set { _target = value; }
        }
        public TextBlock text1 { get; set; }
        public Button btn1 { get; set; }
        public Button btn2 { get; set; }
        public Image image1 { get; set; }

        public object FindName(string propName)
        {
            // var mi = this.Target.GetType().GetRuntimeMethod("FindName", new Type[] { typeof(string) });
            // return mi.Invoke(this.Target, new object[] { propName });
            return this.Target.FindName(propName);
        }

        public MainPage(object target)
        {
            _target = target;
            var obj = FindName("text1");

            /// 1.直接キャストはできない
            // this.text1 = obj as TextBlock;

            /// 2.dynamic に直して Text プロパティで設定可能
            // dynamic o = obj;
            // o.Text = "Dynamic new Message";

            /// 3.型チェックを有効にするために、一度 ITextBlock に直す
            this.text1 = new TextBlock(obj);
            this.text1.Text = "Reflection new Message.";

            this.btn1 = new Button(FindName("btn1"));
            this.btn1.Click += btn1_Click;
            this.btn2 = new Button(FindName("btn2"));
            this.btn2.Click += btn2_Click;
            this.image1 = new Image(FindName("image1"));
        }

        /// <summary>
        /// PCL から直接呼び出す
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn1_Click(object sender, EventArgs e)
        {
            this.text1.Text = "click event.";
        }
        /// <summary>
        /// PCL 内で Image, BitmapImage を直接扱う
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn2_Click(object sender, EventArgs e)
        {
            this.image1.Source = new BitmapImage(
                new Uri("ms-appx:///images/Hidamari-200x160.png"));
        }
    }
#endif
}

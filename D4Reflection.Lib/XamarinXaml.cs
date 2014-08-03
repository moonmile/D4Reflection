using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace D4Reflection.Lib.Xamarin.Xaml
{
    public interface ILabel
    {
        string Text { get; set; }
    }
    public interface IButton
    {
        string Text { get; set; }
    }
    public interface IImageSource { }

    public interface IImage
    {
        IImageSource Source { get; set; }
    }

    public class Label : BindObject, ILabel
    {
        public Label(object target) : base(target) { }
        public string Text
        {
            get { return this.Target.Text; }
            set { this.Target.Text = value; }
        }
    }
    public class Button : BindObject, IButton
    {
        public Button(object target)
            : base(target)
        {
            this.AddEventHandler("Clicked", (s, e) => this.Clicked.Invoke(this, e as EventArgs));
        }
        public string Text
        {
            get { return this.Target.Text; }
            set { this.Target.Text = value; }
        }
        public event EventHandler<EventArgs> Clicked;
    }
    public class ImageSource : BindObject, IImageSource
    {
        public ImageSource(object target)
            : base(target)
        {
        }
        public static ImageSource FromFile( string path ) 
        {
            // こうすると動的ロードできる
            // var t = Type.GetType("Xamarin.Forms.ImageSource, Xamarin.Forms.Core, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null");
            var t = Type.GetType("Xamarin.Forms.ImageSource, Xamarin.Forms.Core");
            var mi = t.GetRuntimeMethod("FromFile", new Type[] { typeof(string) });
            var obj = mi.Invoke(null, new object[] { path });
            return new ImageSource(obj);
        }
    }
    public class Image : BindObject, IImage
    {
        public Image(object target) : base(target) { }
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
}

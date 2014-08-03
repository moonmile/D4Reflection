using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace D4Reflection.Lib.Android.UI
{
    public interface ITextView
    {
        string Text { get; set; }
    }
    public interface IButton
    {
        string Text { get; set; }
    }
    public interface IImageSource { }

    public interface IImageView
    {
        void SetImageResource(int id);
    }

    public class TextView : BindObject, ITextView
    {
        public TextView(object target) : base(target) { }
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
            this.AddEventHandler("Click", (s, e) => this.Click.Invoke(this, e as EventArgs));
        }
        public string Text
        {
            get { return this.Target.Text; }
            set { this.Target.Text = value; }
        }
        public event EventHandler<EventArgs> Click;
    }

    public class ImageView : BindObject, IImageView
    {
        public ImageView(object target) : base(target) { }

        public void SetImageResource(int id)
        {
            this.Target.SetImageResource(id);
        }
    }

    public static class Resource
    {
        public static void Init(object page)
        {
            var qname = page.GetType().AssemblyQualifiedName;
            // ex. "D4Reflection.Droid.MainActivity, D4Reflection.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            var nm = qname.Split(new string[] { ", " }, StringSplitOptions.None);
            var cname = nm[0].Substring(0, nm[0].LastIndexOf('.')) + ".Resource+Drawable";
            var aname = nm[1];
            Resource.DrawableType = Type.GetType(cname + ", " + aname);
        }
        public static Type DrawableType { get; set; }
        public static int Drawable(string name)
        {
            var t = DrawableType;
            if (t != null)
            {
                var fi = t.GetRuntimeField(name);
                if (fi != null)
                {
                    var obj = fi.GetValue(null);
                    if (obj != null)
                    {
                        return (int)obj;
                    }
                }
            }
            return 0;
        }
    } 
}

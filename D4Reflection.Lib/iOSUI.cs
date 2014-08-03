using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace D4Reflection.Lib.iOS.UI
{
    public interface IUILabel
    {
        string Text { get; set; }
    }
    public interface IUIButton
    {
        string Text { get; set; }
    }
    public interface IImageSource { }

    public interface IUIImageView
    {
        void SetImageResource(int id);
    }

    public class UILabel : BindObject, IUILabel
    {
        public UILabel(object target) : base(target) { }
        public string Text
        {
            get { return this.Target.Text; }
            set { this.Target.Text = value; }
        }
    }
    public class UIButton : BindObject, IUIButton
    {
        public UIButton(object target)
            : base(target)
        {
            this.AddEventHandler("TouchUpInside", (s, e) => this.TouchUpInside.Invoke(this, e as EventArgs));
        }
        public string Text
        {
            get { return this.Target.Text; }
            set { this.Target.Text = value; }
        }
        public event EventHandler<EventArgs> TouchUpInside;
    }

    public class UIImageView : BindObject, IUIImageView
    {
        public UIImageView(object target) : base(target) { }

        public void SetImageResource(int id)
        {
            this.Target.SetImageResource(id);
        }
    }
}

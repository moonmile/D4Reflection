using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Reflection;
using System.Linq;

namespace D4Reflection.iPhone
{
    public partial class D4ReflectioniPhoneViewController : UIViewController
    {
        public D4ReflectioniPhoneViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            // Reflection binding.
            new D4ReflectionXamarin.Lib.MainViewController(this);

//            var obj = this.FindTarget("text1");


        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion

        /// <summary>
        /// Private field を取るための補助関数
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public object FindTarget( string propName ) {
            Type t = this.GetType();
            var fis = t.GetFields(BindingFlags.GetField |
                BindingFlags.Instance |
                BindingFlags.NonPublic);
            var fi = fis.First<FieldInfo>(n => n.Name.StartsWith("<"+propName+">"));
            /*
            var fi = t.GetField(propName,
                BindingFlags.GetField |
                BindingFlags.Instance |
                BindingFlags.NonPublic);
            */
            if (fi != null)
            {
                var obj = fi.GetValue(this);
                return obj;
            }
            return null;
        }

        partial void btn0_TouchUpInside(UIButton sender)
        {
            text1.Text = "new message.";

        }
    }
}
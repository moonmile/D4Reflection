using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using D4Reflection.Lib;
using D4Reflection.Lib.iOS.UI;

namespace D4ReflectionXamarin.Lib
{
    /// <summary>
    /// iOS ViewController を Wrapping するクラス
    /// </summary>
    public class MainViewController : BindObject
    {
        public UILabel text1 { get; set; }
        public UIButton btn1 { get; set; }
        public UIButton btn2 { get; set; }
        public UIImageView image1 { get; set; }

        public MainViewController(object target)
            : base(target)
        {
            this.text1 = new UILabel(FindName("text1"));
            this.text1.Text = "Reflection new Message.";

            this.btn1 = new UIButton(FindName("btn1"));
            this.btn1.TouchUpInside += btn1_Click;
            this.btn2 = new UIButton(FindName("btn2"));
            this.btn2.TouchUpInside += btn2_Click;
            this.image1 = new UIImageView(FindName("image1"));
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
            // image1.SetImageResource(Resource.Drawable.Hidamari_200x160);
            // image1.SetImageResource(Resource.Drawable("Hidamari_200x160"));
        }
    }
}

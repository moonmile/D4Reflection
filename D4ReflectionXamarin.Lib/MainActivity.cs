using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using D4Reflection.Lib;
using D4Reflection.Lib.Android.UI;

namespace D4ReflectionXamarin.Lib
{
    /// <summary>
    /// Android MainActivity を Wrapping するクラス
    /// </summary>
    public class MainActivity : BindObject
    {
        public TextView text1 { get; set; }
        public Button btn1 { get; set; }
        public Button btn2 { get; set; }
        public ImageView image1 { get; set; }

        public MainActivity(object target)
            : base(target)
        {
            Resource.Init(target);
            this.text1 = new TextView(FindName("text1"));
            this.text1.Text = "Reflection new Message.";

            this.btn1 = new Button(FindName("btn1"));
            this.btn1.Click += btn1_Click;
            this.btn2 = new Button(FindName("btn2"));
            this.btn2.Click += btn2_Click;
            this.image1 = new ImageView(FindName("image1"));
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
            image1.SetImageResource(Resource.Drawable("Hidamari_200x160"));
        }
    }
}

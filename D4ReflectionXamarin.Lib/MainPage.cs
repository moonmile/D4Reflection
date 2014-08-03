using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using D4Reflection.Lib;
using D4Reflection.Lib.Xamarin.Xaml;

namespace D4ReflectionXamarin.Lib
{

    /// <summary>
    /// Xamarin.Forms の MainPage を Wrapping するクラス
    /// </summary>
    public class MainPage : BindObject
    {
        public Label text1 { get; set; }
        public Button btn1 { get; set; }
        public Button btn2 { get; set; }
        public Image image1 { get; set; }

        public MainPage(object target)
            : base(target)
        {
            var obj = FindName("text1");

            this.text1 = new Label(obj);
            this.text1.Text = "Reflection new Message.";

            this.btn1 = new Button(FindName("btn1"));
            this.btn1.Clicked += btn1_Click;
            this.btn2 = new Button(FindName("btn2"));
            this.btn2.Clicked += btn2_Click;
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
            var obj = ImageSource.FromFile("Hidamari_200x160.png");
            this.image1.Source = obj;
        }
    }
}

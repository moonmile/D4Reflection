using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using D4Reflection.Lib;
using D4Reflection.Lib.Windows.Xaml;

namespace D4ReflectionCSharp.Lib
{
    public class MainPage : BindObject
    {
        public TextBlock text1 { get; set; }
        public Button btn1 { get; set; }
        public Button btn2 { get; set; }
        public Image image1 { get; set; }

        public MainPage(object target) : base( target )
        {
            var obj = base.FindName("text1");

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

}

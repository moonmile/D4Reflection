using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace D4Reflection
{
    /// <summary>
    /// Frame 内へナビゲートするために利用する空欄ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        D4ReflectionCSharp.Lib.MainPage page;
        public MainPage()
        {
            this.InitializeComponent();

            // PLC内へバインド
            this.page = new D4ReflectionCSharp.Lib.MainPage(this);
        }

        /// <summary>
        /// 通常のコードビハイド
        /// btn1, btn2 の Click イベントは D4ReflectionCSharp.Lib.MainPage 内で行う
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.text1.Text = "new message.";
        }
    }
}

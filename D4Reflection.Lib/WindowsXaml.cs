using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D4Reflection.Lib.Extentions;


namespace D4Reflection.Lib.Windows.Xaml
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
            this.Target = t.CreateInstance(uri);
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
}

D4Reflection
===============
Dirty Deeds Done Dirt Reflection
いともたやすく行われるえげつないリフレクション

# 能力

PCL(Portable Class Library)の中から、フロントエンド（Windows Store 8.1 や Windows Phone 8.1など）の
オブジェクトを動的バインド＆動的ローディングして操作する。

もともと、F# 用にリフレクションを使って実装したものを dynamic を使って C# に直してみた。

# 使い方

PCL 内で、元の MainPage を模倣して作成したのちプロパティ等を使う。
Click イベントの引数は RoutedEventArgs になるが、EventArgs にして仮実装。

```csharp
public MainPage(object target)
{
    _target = target;
    var obj = FindName("text1");

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
```

本来 PCL 内では使えない Windows.UI.Xaml.Media.Imaging.BitmapImage なども利用できる。

ポータブルクラスライブラリをターゲットを Profile259 にして作成できる。

- .NET Framework 4.5
- Windows 8
- Windows Phone Silverlight 8
- Windows Phone 8.1
- Xamarin.Android
- Xamarin.iOS

# 今後の予定

- Windows.Foundation.*
- Windows.UI.Xaml.*

あたりを、ごっそり実装予定。


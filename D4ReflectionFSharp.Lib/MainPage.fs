namespace D4ReflectionFSharp.Lib
open System
open System.Collections.Generic
open System.Linq
open System.Text
open D4Reflection.Lib
open D4Reflection.Lib.Windows.Xaml

type MainPage(target:obj) =
    inherit BindObject(target)

    let mutable text1 = new TextBlock(base.FindName("text1"))
    let mutable btn1  = new Button(base.FindName("btn1"))
    let mutable btn2  = new Button(base.FindName("btn2"))
    let mutable image1  = new Image(base.FindName("image1"))

    do
        btn1.Click.Add( fun(e) -> 
            text1.Text <- "click event F#"
        )
        btn2.Click.Add( fun(e) -> 
            image1.Source <- new BitmapImage(
                new Uri("ms-appx:///images/HaruLock.png"))
        )
    

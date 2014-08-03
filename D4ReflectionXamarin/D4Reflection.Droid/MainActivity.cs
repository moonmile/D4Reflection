using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace D4Reflection.Droid
{
    [Activity(Label = "D4Reflection.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public Button btn0, btn1, btn2;
        public ImageView image1;
        public TextView text1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            this.btn0 = FindViewById<Button>(Resource.Id.MyButton);
            this.btn1 = FindViewById<Button>(Resource.Id.button1);
            this.btn2 = FindViewById<Button>(Resource.Id.button2);
            this.image1 = FindViewById<ImageView>(Resource.Id.imageView1);
            this.text1 = FindViewById<TextView>(Resource.Id.textView1);

            this.btn0.Click += btn0_Click;

            // Reflection binding
            new D4ReflectionXamarin.Lib.MainActivity(this);

        }

        void btn0_Click(object sender, EventArgs e)
        {
            text1.Text = "new message.";
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Xamarin.Forms;
using System.Diagnostics;

namespace D4ReflectionXamarin
{
    public partial class MainPage
    {
        Lib.MainPage page;

        public MainPage()
        {
            InitializeComponent();
            page = new Lib.MainPage(this);
        }
        void btn0_Clicked(object sender, EventArgs e)
        {
            this.text1.Text = "new message.";

            // var name = typeof(ImageSource).AssemblyQualifiedName;
            // this.image1.Source = ImageSource.FromFile("Images/Hidamari-200x160.png");

        }
    }
}

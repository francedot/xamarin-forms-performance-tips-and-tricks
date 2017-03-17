using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppDemo0
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            var watch = Stopwatch.StartNew();
            InitializeComponent();
            watch.Stop();
            Debug.WriteLine("Elapsed: " + watch.ElapsedMilliseconds + "ms");
        }
    }
}

using System;
using Android.Views;

namespace AppDemo5.Droid
{
    public class RecyclerClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}


using System;
using Xamarin.Forms;

namespace AppDemo5
{
    public class FormsApp : Application
    {
        public FormsApp(Type pageType = null)
        {
            SetMainPage(pageType);
        }

        public void SetMainPage(Type pageType)
        {
            if (pageType == null) // To manage UWP and iOS
            {
                MainPage = new ContentPage();
                return;
            }

            MainPage = (Page)Activator.CreateInstance(pageType);
        }

        public static Page GetPage<T>() where T : Page // To Manage iOS
        {
            return Activator.CreateInstance<T>();
        }
    }
}
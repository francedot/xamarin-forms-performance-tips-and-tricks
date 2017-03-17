using System;
using Windows.UI.Xaml.Navigation;

namespace AppDemo5.UWP
{
    public partial class FormsWrapperPage
    {
        private readonly FormsApp _formsApp;

        public FormsWrapperPage()
        {
            this.InitializeComponent();
            LoadApplication(_formsApp = new FormsApp());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _formsApp.SetMainPage(e.Parameter as Type);
        }
    }
}

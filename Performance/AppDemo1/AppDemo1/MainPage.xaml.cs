using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppDemo1
{
    public partial class MainPage : ContentPage
    {
        readonly MainPageViewModel _viewModel;

        public MainPage()
        {
            this.BindingContext = _viewModel = new MainPageViewModel();
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnAppearingAsync();
        }
    }
}

using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppDemo3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            var stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            stackLayout.Children.Add(new ActivityIndicator
            {
                IsRunning = true
            });
            stackLayout.Children.Add(new Label
            {
                Text = $"Greetings from {(ThreadHelper.IsMainThread() ? "Main Thread" : "Background Thread")}"
            });

            Content = stackLayout;

            // On Main Thread
            var inflatedView = InflateOnMainThread();
            AddToVisualTree(inflatedView);

            // On Background Thread
            //InflateOnBackgroundThread()
            //    .ContinueWith(task => AddToVisualTree(task.Result),
            //        TaskScheduler.FromCurrentSynchronizationContext());
        }

        private StackLayout InflateOnMainThread()
        {
            var stackLayout = new StackLayout();
            stackLayout.Children.Add(new ColorfulClock());
            stackLayout.Children.Add(new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                Text = $"Greetings from {(ThreadHelper.IsMainThread() ? "Main Thread" : "Background Thread")}!"
            });

            return stackLayout;
        }

        private Task<StackLayout> InflateOnBackgroundThread()
        {
             return Task.Factory.StartNew(() =>
                {
                    var stackLayout = new StackLayout();
                    stackLayout.Children.Add(new ColorfulClock());
                    stackLayout.Children.Add(new Label
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        FontSize = 20,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.Black,
                        Text = $"Greetings from {(ThreadHelper.IsMainThread() ? "Main Thread" : "Background Thread")}!"
                    });

                    return stackLayout;
                });
        }

        private void AddToVisualTree(View view)
        {
            Content = view;
        }
    }
}

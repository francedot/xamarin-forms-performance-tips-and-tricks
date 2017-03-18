using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AppDemo4
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            // Scenario 0 (GOOD) - Inflate and add to VT in the Constructor
            // At Constructor-Time no VT has been created
            //ID                                            | Call Count
            //InvalidateMeasureInternal                     | 20        
            //UpdateChildrenLayout                          | 1         
            //InflateAndAddToVt0Good();

            // Scenario 1 - 14: InitializeComponent() Time Profiling for the different scenarios
            InflateAndAddToVt1();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Scenario 0 (BAD, Really) - Inflate and add to VT in OnAppearing
            // At OnAppearing-Time the MainPage VT has been created.
            // Therefore, after the Content has been set, any consequential changes on its child nodes, will invalidate the layout multiple times.
            
            //ID                        | Call Count
            //InvalidateMeasureInternal | 26       
            //UpdateChildrenLayout      | 1         
            //InflateAndAddToVt0Bad();

            //ID                        | Call Count
            //InvalidateMeasureInternal | 20       
            //UpdateChildrenLayout      | 1         
            //InflateAndAddToVt0Good();
        }

        void InflateAndAddToVt0Good()
        {
            var grid = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };

            var label1 = new Label
            {
                Text = "Hello Codemotion!"
            };
            var label2 = new Label
            {
                Text = "Hello Codemotion!"
            };
            var label3 = new Label
            {
                Text = "Hello Codemotion!"
            };
            var label4 = new Label
            {
                Text = "Hello Codemotion!"
            };
            grid.Children.Add(label1);
            grid.Children.Add(label2);
            grid.Children.Add(label3);
            grid.Children.Add(label4);
            Grid.SetRow(label1, 0);
            Grid.SetRow(label2, 1);
            Grid.SetRow(label3, 2);
            Grid.SetRow(label4, 3);

            Content = grid;

            Device.StartTimer(TimeSpan.FromSeconds(4), () =>
            {
                LayoutProfiler.DumpStats();

                return false;
            });
        }

        void InflateAndAddToVt0Bad()
        {
            var grid = new Grid();
            Content = grid;
            grid.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto }
            };

            var label1 = new Label
            {
                Text = "Hello Codemotion!"
            };
            var label2 = new Label
            {
                Text = "Hello Codemotion!"
            };
            var label3 = new Label
            {
                Text = "Hello Codemotion!"
            };
            var label4 = new Label
            {
                Text = "Hello Codemotion!"
            };
            grid.Children.Add(label1);
            grid.Children.Add(label2);
            grid.Children.Add(label3);
            grid.Children.Add(label4);

            Grid.SetRow(label1, 0);
            Grid.SetRow(label2, 1);
            Grid.SetRow(label3, 2);
            Grid.SetRow(label4, 3);

            Device.StartTimer(TimeSpan.FromSeconds(4), () =>
            {
                LayoutProfiler.DumpStats();

                return false;
            });
        }

        void InflateAndAddToVt1()
        {
            using (new TimeProfiler("MainPage.InitializeComponent()"))
            {
                InitializeComponent();
            }

            Device.StartTimer(TimeSpan.FromSeconds(4), () =>
            {
                //UpdateLabel2Scenario();

                LayoutProfiler.DumpStats();

                return false;
            });
        }

        //void UpdateLabel2Scenario()
        //{
        //    Label2.Text = "Label2 Text has changed!";
        //}
    }
}

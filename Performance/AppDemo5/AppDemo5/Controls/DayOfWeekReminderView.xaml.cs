using System;
using System.Globalization;
using Xamarin.Forms;

namespace AppDemo5.Controls
{
    public partial class DayOfWeekReminderView : ContentView
    {
        public static readonly BindableProperty IsReminderOnProperty = BindableProperty.Create(nameof(IsReminderOn), typeof(bool), typeof(DayOfWeekReminderView), default(bool), propertyChanged: IsReminderOnPropertyChanged, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty DayOfWeekProperty = BindableProperty.Create(nameof(DayOfWeek), typeof(DayOfWeek?), typeof(DayOfWeekReminderView), default(DayOfWeek?), propertyChanged:DayOfWeekPropertyChanged);
        public static readonly BindableProperty DayReminderTimeProperty = BindableProperty.Create(nameof(DayReminderTime), typeof(TimeSpan), typeof(DayOfWeekReminderView), default(TimeSpan), propertyChanged: DayReminderTimePropertyChanged, defaultBindingMode:BindingMode.TwoWay);

        private static readonly DateTimeFormatInfo DateTimeFormats;

        static DayOfWeekReminderView()
        {
            var cultureInfo = new CultureInfo("it-IT");
            DateTimeFormats = cultureInfo.DateTimeFormat;
        }

        public DayOfWeekReminderView()
        {
            InitializeComponent();
        }

        public bool IsReminderOn
        {
            get { return (bool)GetValue(IsReminderOnProperty); }
            set { SetValue(IsReminderOnProperty, value); }
        }

        public DayOfWeek? DayOfWeek
        {
            get { return (DayOfWeek?)GetValue(DayOfWeekProperty); }
            set { SetValue(DayOfWeekProperty, value); }
        }

        public TimeSpan DayReminderTime
        {
            get { return (TimeSpan)GetValue(DayReminderTimeProperty); }
            set { SetValue(DayReminderTimeProperty, value); }
        }

        private static void IsReminderOnPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var view = bindable as DayOfWeekReminderView;
            if (view == null)
            {
                return;
            }
            view.RemainderSwitch.IsToggled = (bool) newvalue;
        }

        private static void DayOfWeekPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as DayOfWeekReminderView;
            if (view == null)
            {
                return;
            }
            if (newValue == null)
            {
                return;
            }
            view.DayOfWeekLabel.Text = DateTimeFormats.DayNames[(int)newValue];
        }

        private static void DayReminderTimePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var view = bindable as DayOfWeekReminderView;
            if (view == null)
            {
                return;
            }
            view.DayReminderTimeSpan.Time = (TimeSpan)newvalue;
            bindable.SetValue(DayReminderTimeProperty, newvalue);
        }
    }
}

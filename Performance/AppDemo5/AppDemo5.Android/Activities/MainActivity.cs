using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using AppDemo5.Helpers;
using AppDemo5.Pages;
using Xamarin.Forms.Platform.Android;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace AppDemo5.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.activity_main;

        private ViewPager _pager;
        private TabsAdapter _adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _adapter = new TabsAdapter(this, SupportFragmentManager);
            _pager = FindViewById<ViewPager>(Resource.Id.viewpager);
            var tabs = FindViewById<TabLayout>(Resource.Id.tabs);
            _pager.Adapter = _adapter;
            tabs.SetupWithViewPager(_pager);
            _pager.OffscreenPageLimit = 3;

            _pager.PageSelected += (sender, args) =>
            {
                var fragment = _adapter.InstantiateItem(_pager, args.Position) as IFragmentVisible;

                fragment?.BecameVisible();
            };

            Toolbar.MenuItemClick += ToolbarOnMenuItemClick;

            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(false);
        }

        private void ToolbarOnMenuItemClick(object sender, Toolbar.MenuItemClickEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.menu_edit:
                {
                    var intent = new Intent(this, typeof(AddItemActivity)); ;
                    StartActivity(intent);
                    break;
                }
                case Resource.Id.menu_settings:
                {
                    var intent = new Intent(this, typeof(FormsActivity));
                    intent.PutExtra("PageType", "SettingsPage");
                    StartActivity(intent);
                    break;
                }
                             
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

    }

    public class TabsAdapter : FragmentStatePagerAdapter
    {
        readonly string[] _titles;

        public override int Count => _titles.Length;

        public TabsAdapter(Context context, Android.Support.V4.App.FragmentManager fm) : base(fm)
        {
            _titles = context.Resources.GetTextArray(Resource.Array.sections);
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position) =>
                            new Java.Lang.String(_titles[position]);

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0: return BrowseFragment.NewInstance();
                case 1: return AboutFragment.NewInstance();
            }
            return null;
        }

        public override int GetItemPosition(Java.Lang.Object frag) => PositionNone;

    }

}



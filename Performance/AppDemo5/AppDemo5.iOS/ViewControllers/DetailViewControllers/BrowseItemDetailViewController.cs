using System;
using UIKit;

using AppDemo5.ViewModel;

namespace AppDemo5.iOS
{
    public partial class BrowseItemDetailViewController : UIViewController
    {
		public ItemDetailViewModel ViewModel { get; set; }
		public BrowseItemDetailViewController(IntPtr handle) : base(handle) { }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = ViewModel.Title;
			ItemNameLabel.Text = ViewModel.Item.Text;
			ItemDescriptionLabel.Text = ViewModel.Item.Description;

		}


    }
}
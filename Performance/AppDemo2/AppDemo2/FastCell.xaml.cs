using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppDemo2
{
    public partial class FastCell : ViewCell
    {
        public FastCell()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var item = BindingContext as Movie;
            if (item == null)
            {
                return;
            }

            PosterImage.Source = ImageSource.FromUri(new Uri(item.Poster));
            TitleLabel.Text = item.Title;
            DirectorLabel.Text = item.Director;
            CountryLabel.Text = item.Country;
            YearLabel.Text = item.Year;
            RatingLabel.Text = $"{item.Rating:0.0}";
        }
    }
}

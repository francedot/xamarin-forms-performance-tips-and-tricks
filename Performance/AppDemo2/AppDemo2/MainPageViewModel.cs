using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppDemo2
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly MoviesLoader _moviesLoader = new MoviesLoader();

        //private IEnumerable<Movie> _movies;
        private IList<Movie> _movies;

        //public IEnumerable<Movie> Movies
        //{
        //    get { return _movies; }
        //    set
        //    {
        //        _movies = value;
        //        OnPropertyChanged();
        //    }
        //}

        public IList<Movie> Movies
        {
            get { return _movies; }
            set
            {
                _movies = value;
                OnPropertyChanged();
            }
        }



        public async Task OnAppearingAsync()
        {
            Movies = await _moviesLoader.LoadMoviesAsync();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

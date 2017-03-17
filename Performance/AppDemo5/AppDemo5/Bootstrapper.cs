using AppDemo5.Helpers;
using AppDemo5.Interfaces;
using AppDemo5.Services;
using AppDemo5.Model;

namespace AppDemo5
{
    public partial class Bootstrapper
    {
        public Bootstrapper()
        {
        }

        /// <summary>
        /// DI Registration
        /// </summary>
        public static void Initialize()
        {
            ServiceLocator.Instance.Register<IDataStore<Item>, MockDataStore>();
        }
    }
}
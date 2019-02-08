using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Extended;

namespace SearcherApp
{
   public class MainNamesViewModel : INotifyPropertyChanged
    {
        private bool isBusy;
        private const int sizePage = 18;
        readonly NameService dataService = new NameService();

        public InfiniteScrollCollection<string> Names { get; set; }

        public bool IsBusy
        {
            get => isBusy;

            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        public MainNamesViewModel()
        {
            Names = new InfiniteScrollCollection<string>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    var page = Names.Count / sizePage;

                    var nameItems = await dataService.GetNamesAsync(page, sizePage);

                    IsBusy = false;

                    return nameItems;
                },

                OnCanLoadMore = () =>
                {
                    return Names.Count() < 44;
                }
            };


            DownloadDataAsync();
        }

        private async Task DownloadDataAsync()
        {
            var items = await dataService.GetNamesAsync(index: 0, size: sizePage);

            Names.AddRange(items);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

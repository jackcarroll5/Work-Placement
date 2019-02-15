using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LightMVVM
{
   public class ListViewModel : ViewModelBase
    {
        public ListViewModel()
        {
            NewItem = "";

            Items = new ObservableCollection<ToDoItem>()
            {
                new ToDoItem()
                {
                    Name = "Fix a bug",
                },

                 new ToDoItem()
                {
                    Name = "Deploy app",
                },

                  new ToDoItem()
                {
                    Name = "Make millions",
                },
            };
        }

        public ObservableCollection<ToDoItem> Items
        {
            get;
            set;
        }

        public String NewItem
        {
            get;
            set;
        }

        private RelayCommand addItem;

        public RelayCommand AddItem
        {
            get
            {
                return addItem ?? (addItem = new RelayCommand(() => AddNewItem()));
            }
        }

        private ObservableCollection<ToDoItem> items;

        private void AddNewItem()
        {
            if(NewItem.Trim().Length > 0)
            {
                Items.Add(new ToDoItem() { Name = NewItem });
                NewItem = "";
                RaisePropertyChanged("NewItem");
            }
        }
    }
}

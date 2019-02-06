using MVVMXamarinApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MVVMXamarinApp.ViewModels
{
   public class HomeViewModel : INotifyPropertyChanged
    {
        private TaskModel _taskModelType;
        private string _message;

        public TaskModel TaskModel
        {
            get { return _taskModelType; }
            set
            {
                _taskModelType = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public Command SaveCommand
        {
            get
            {
               return new Command(() =>
                {
                    Message = "Your task : " + TaskModel.Title + " ,"
                    + TaskModel.DurationTime + " was saved successfully!";
                });
            }

        }

        public HomeViewModel()
        {
            TaskModel = new TaskModel
            {
                Title = "Create UI",
                DurationTime = 2
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

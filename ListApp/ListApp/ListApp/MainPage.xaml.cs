using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            MainList.ItemsSource = new List<Person>
            {
                new Person
                {
                    Name = "Jack",
                    Age = 22
                },
                new Person
                {
                    Name = "Megan",
                    Age = 21
                },
                new Person
                {
                    Name = "Adam",
                    Age = 20
                },
                new Person
                {
                    Name = "Mike",
                    Age = 19
                },
                new Person
                {
                    Name = "Ellie",
                    Age = 21
                }
            };

        }
    }
}

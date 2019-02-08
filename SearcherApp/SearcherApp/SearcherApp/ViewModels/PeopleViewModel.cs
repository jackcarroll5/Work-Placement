using SearcherApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;

namespace SearcherApp.ViewModels
{
    public class PeopleViewModel
    {
        public ObservableCollection<People> People { get; set; }

        public Command<People> RemoveCommand
        {
            get
            {
                return new Command<People>((people) =>
                {
                    People.Remove(people);
                });
            }
        }

        public PeopleViewModel()
        {
            People = new ObservableCollection<People> {
                new People {
                    Name = "Ben",
                        Age = 36
                },
                new People {
                    Name = "Rebecca",
                         Age = 20
                },
                new People {
                    Name = "Shania",
                         Age = 21
                },
                new People {
                    Name = "Gary",
                       Age = 30
                },
                new People {
                    Name = "Martin",
                        Age = 40
                },
                new People {
                    Name = "Jennifer",
                        Age = 19
                },
                new People {
                    Name = "Dave",
                        Age = 43
                },
                 new People {
                    Name = "Bradley",
                        Age = 39
                }
            };


        }
    }
}

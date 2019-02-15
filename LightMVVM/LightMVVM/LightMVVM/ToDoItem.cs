using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;

namespace LightMVVM
{
    public class ToDoItem
    {
        public ToDoItem()
        {

        }

        public String Name
        {
            get;
            set;
        }

        public bool Finished
        {
            get;
            set;
        }

    }
}

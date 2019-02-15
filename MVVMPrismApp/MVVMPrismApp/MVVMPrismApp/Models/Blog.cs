using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMPrismApp.Models
{
    public class Blog
    {
        public string BlogTitle { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string Creator { get; set; }
    }
}

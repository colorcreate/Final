using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final.Models
{
    public class PaketToko
    {
        public CustomerModel Customer { get; set; }
        public List<TokoModel> Tokos { get; set; }
    }
}
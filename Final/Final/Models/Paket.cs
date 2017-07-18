using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final.Models
{
    public class Paket
    {
        public TokoModel Toko { get; set; }
        public List<MenuModel> Menus { get; set; }
        public List<KategoryModel> Kategories { get; set; }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final.Models
{
    public class MenuModel
    {
        public int Id { get; set; }
        public int IdToko { get; set; }
        public int IdMenu { get; set; }
        public string NameMenu { get; set; }
        public int IdTypeMenu { get; set; }
        public int Harga { get; set; }
        public string GambarMenu { get; set; }
    }
}
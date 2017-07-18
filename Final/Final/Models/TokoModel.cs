using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final.Models
{
    public class TokoModel
    {
        public int IdToko { get; set; }
        public string NamaToko { get; set; }
        public string DeskripsiToko { get; set; }
        public string JenisMakananToko { get; set; }
        public string AlamatToko { get; set; }
        public string JadwalToko { get; set; }
        public string Gambar { get; set; }
    }
}
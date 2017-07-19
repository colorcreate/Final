using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final.Models
{
    public class PesanModel
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string Menu { get; set; }
        public string Toko { get; set; }
        public int Harga { get; set; }
    }
}
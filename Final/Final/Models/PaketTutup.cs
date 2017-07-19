using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final.Models
{
    public class PaketTutup
    {
        public List<PesanModel> Pesanan { get; set; }
        public CustomerModel customer { get; set; }
    }
}
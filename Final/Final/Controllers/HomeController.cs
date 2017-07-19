using Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final.Controllers
{
    public class HomeController : Controller
    {
        private OperationDataContext context;
        public HomeController()
        {
            context = new OperationDataContext();
        }

        public ActionResult Login(int? Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Login(CustomerModel model)
        {
            string name = model.Name;
            string password = model.Password;
            var a = from b in context.Customers where b.Name == model.Name && b.Password == model.Password select b;
            if (a.Any())
            {
                int Id=0;
                foreach (var item in a)
                    Id = item.Id;

                return RedirectToAction("Toko", "Home", new { Id});
            }
            else
            {
                return RedirectToAction("Login", new { Id = 1 });
            }
        }

        public ActionResult Register(int? Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Register(CustomerModel model)
        {
            try
            {
                Customer customer = new Customer()
                {
                    Name = model.Name,
                    Password = model.Password,
                    Email = model.Email,
                    Phone = model.Phone,
                    Address = model.Address
                };
                 
                context.Customers.InsertOnSubmit(customer);
                context.SubmitChanges();

                return RedirectToAction("Login");
            }
            catch
            {
                return RedirectToAction("Register", new { Id = 1 });
            }
            
        }


        



        public ActionResult Toko(int Id)
        {
            PaketToko paket = new PaketToko();
            var customer = customerlist(Id).ElementAt(0);
            var tokoList = tokolist();

            paket.Customer = customer;
            paket.Tokos = tokoList;
            return View(paket);
        }

        public ActionResult Menu(int IdToko, int Id)
        {
            Paket paket = new Paket();
            var tokoList = tokolist();
            var menuList = menulist(IdToko);
            var kategoryList = kategorylist(IdToko);            
            var toko = from item in tokoList where item.IdToko == IdToko select item;
            var customer = customerlist(Id).ElementAt(0);



            paket.Toko = toko.ElementAt(0);
            paket.Menus = menuList;
            paket.Kategories = kategoryList;
            paket.Customer = customer;


            return View(paket);
        }

        public ActionResult Pesan(int IdCustomer, int IdToko, int IdMenu, int? cek, int Saldo)
        {
            ViewBag.IdCustomer = IdCustomer;
            ViewBag.IdToko = IdToko;
            ViewBag.IdMenu = IdMenu;
            ViewBag.Saldo = Saldo;

            if(cek==null)
            {
                var tokoList = tokolist();
                var toko = from item in tokoList where item.IdToko == IdToko select item;
                var menuList = menulist(IdToko);
                var menu = from item in menuList where item.IdMenu == IdMenu select item;

                Pesan pesan = new Pesan()
                {
                    Customer = customerlist(IdCustomer).ElementAt(0).Name,
                    Menu = menu.ElementAt(0).NameMenu,
                    Toko = toko.ElementAt(0).NamaToko,
                    harga = menu.ElementAt(0).Harga
                };

                context.Pesans.InsertOnSubmit(pesan);
                context.SubmitChanges();
            }
            

            var model = pesanlist();
            var total = 0;
            foreach(var a in model)
            {
                total += a.Harga;
            }
            ViewBag.total = total;
            return View(model);
        }


        public ActionResult Delete ()
        {
            List<Pesan> All = (from item in context.Pesans select item).ToList();
            context.Pesans.DeleteAllOnSubmit(All);
            context.SubmitChanges();

            return RedirectToAction("Login");
        }

        public ActionResult Pesans(int Id, int IdCustomer, int IdToko, int IdMenu, int Saldo)
        {
            
            var pesans = from item in context.Pesans where item.Id == Id select item;
            Pesan pesan = pesans.First();
            context.Pesans.DeleteOnSubmit(pesan);
            context.SubmitChanges();
            return RedirectToAction("Pesan", new { @IdCustomer = IdCustomer, @IdToko = IdToko, @IdMenu = IdMenu, @cek = 1, @Saldo = Saldo });
        }

        public List<TokoModel> tokolist()
        {
            List<TokoModel> tokoList = new List<TokoModel>();

            var query = from toko in context.Tokos select toko;
            var tokos = query.ToList();
            foreach (var a in tokos)
            {
                tokoList.Add(new TokoModel()
                {
                    IdToko = a.IdToko,
                    NamaToko = a.NamaToko,
                    DeskripsiToko = a.DeskripsiToko,
                    JenisMakananToko = a.JenisMakananToko,
                    AlamatToko = a.AlamatToko,
                    JadwalToko = a.JadwalToko,
                    Gambar = a.Gambar

                });
            }
            return tokoList;
        }

        public List<MenuModel> menulist(int IdToko)
        {
            List<MenuModel> menuList = new List<MenuModel>();

            var query = from menu in context.Menus where menu.IdToko == IdToko select menu;
            var menus = query.ToList();
            foreach (var a in menus)
            {
                menuList.Add(new MenuModel()
                {
                    Id = a.Id,
                    IdToko = a.IdToko,
                    IdMenu = a.IdMenu,
                    NameMenu = a.NameMenu,
                    IdTypeMenu = a.IdTypeMenu,
                    Harga = a.Harga,
                    GambarMenu = a.NameMenu +".jpg"
                });
            }

            return menuList;
        }        

        public List<KategoryModel> kategorylist(int IdToko)
        {
            List<KategoryModel> kategoryList = new List<KategoryModel>();

            var query = from kategory in context.Kategories where kategory.IdToko == IdToko select kategory;
            var kategories = query.ToList();
            foreach (var a in kategories)
            {
                kategoryList.Add(new KategoryModel()
                {
                    IdToko = a.IdToko,
                    IdTypeMenu = a.IdTypeMenu,
                    Nama = a.Nama
                });
            }

            return kategoryList;
        }

        public List<CustomerModel> customerlist(int Id)
        {
            List<CustomerModel> customerList = new List<CustomerModel>();
            var query = from customer in context.Customers where customer.Id == Id select customer;
            var customers = query.ToList();
            foreach (var a in customers)
            {
                CustomerModel customer = new CustomerModel();
                customer.Id = a.Id;
                customer.Name = a.Name;
                customer.Password = a.Password;
                customer.Email = a.Email;
                customer.Phone = a.Phone;
                customer.Address = a.Address;
                if (a.Saldo != null) { customer.Saldo = (int)a.Saldo; } else { customer.Saldo = 0; }
                if (a.Budget != null) { customer.Budget = (int)a.Budget; } else { customer.Budget = 0; }
                customerList.Add(customer);
        
            }
            return customerList;
        }

        public List<PesanModel> pesanlist()
        {
            List<PesanModel> pesanList = new List<PesanModel>();
            var query = from item in context.Pesans select item;
            var pesans = query.ToList();

            foreach (var a in pesans)
            {
                PesanModel pesan = new PesanModel();
                pesan.Id = a.Id;
                pesan.Customer = a.Customer;
                pesan.Toko = a.Toko;
                pesan.Menu = a.Menu;
                pesan.Harga = a.harga;
                pesanList.Add(pesan);

            }
            return pesanList;
        }

        
    }
}
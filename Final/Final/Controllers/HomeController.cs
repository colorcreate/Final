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

                Session["User"] = Id;
                return RedirectToAction("Toko", "Home");
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




        public ActionResult Toko()
        {
            var cek = customerlist();
            var tokoList = tokolist();
            return View(tokoList);
        }

        public ActionResult Menu(int IdToko)
        {
            Paket paket = new Paket();
            var tokoList = tokolist();
            var menuList = menulist(IdToko);
            var kategoryList = kategorylist(IdToko);            
            var toko = from item in tokoList where item.IdToko == IdToko select item;
            
            
            

            paket.Toko = toko.ElementAt(0);
            paket.Menus = menuList;
            paket.Kategories = kategoryList;


            return View(paket);
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
                    GambarMenu = a.GambarMenu
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

        public List<CustomerModel> customerlist()
        {
            List<CustomerModel> customerList = new List<CustomerModel>();
            var query = from customer in context.Customers select customer;
            var customers = query.ToList();
            foreach(var a in customers)
            {
                customerList.Add(new CustomerModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Password = a.Password, 
                    Email = a.Email,
                    Phone = a.Phone,
                    Address = a.Address,
                    Saldo = (int)a.Saldo,
                    Budget = (int)a.Budget
                });
            }
            return customerList;
        }
    }
}
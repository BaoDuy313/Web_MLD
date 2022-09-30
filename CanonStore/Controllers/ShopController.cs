using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CanonStore.Models;

namespace CanonStore.Controllers
{
    public class ShopController : Controller
    {
        Canon_StoreEntities4 ctx = new Canon_StoreEntities4();
        // GET: Customer
        public ActionResult Index()
        {
            List<Product> products = ctx.Products.Take(3).ToList();
            return View(products);
        }
        //----------Login----------

        public ActionResult Login()
        {
            if (Session["CustomerId"] != null)
            {
                int id = Convert.ToInt32(Session["CustomerId"]);
                Customer customer = ctx.Customers.Where(c => c.id == id).SingleOrDefault();
                return View("AboutCustomer",customer);
            }
            else
            {
                return View();
            }

            
            
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(CustomerCheck customerCheck)
        {
            if (ModelState.IsValid)
            {
                using (Canon_StoreEntities4 db = new Canon_StoreEntities4())
                {
                    var obj = db.Customers.Where(a => a.UserName == customerCheck.UserName && a.Password == customerCheck.Password).FirstOrDefault();

                    if (obj != null)
                    {
                        Session["CustomerId"] = obj.id.ToString();
                        Session["CustomerUserName"] = obj.UserName.ToString();
                        Session["CustomerName"] = obj.Name.ToString();
                       
                        return RedirectToAction("Index");
                    }
                    else
                    {
                       
                        return View();
                    }
                }

            }


            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
        public ActionResult SignUp()
        {
            Customer customer = new Customer();
            return View(customer);
        }
        [HttpPost]
        public ActionResult SignUpSave(Customer customer)
        {
            if (ModelState.IsValid)
            {
                ctx.Customers.Add(customer);
                ctx.SaveChanges();

                return RedirectToAction("Products");
            }
            else
            {

                return View("SignUp");
            }
        }
            //----------Products----------
        public ActionResult Products()
        {
            List<Product> products = ctx.Products.ToList();
            return View(products);
        }
        public ActionResult AboutUS()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ProductsSearch(int id)
        {
            List<Product> products = ctx.Products.Where(c => c.Type == id).ToList();
            return View(products);
        }
        public ActionResult ProductDetails(int id)
        {
            Product product = ctx.Products.Where(c => c.Id == id).SingleOrDefault();

            List<Product> products3 = ctx.Products.Where(c => c.Type == product.Type && c.Id != product.Id).Take(3).ToList();
            ViewBag.products3 = products3;

            return View(product);
        }
        //----------Cart----------
        public ActionResult Cart()
        {

            ViewBag.yourSessionId = HttpContext.Session.SessionID;

            List<ItemCart> cart = null;
            if (HttpContext.Session["yourcart"] == null)
            {
                cart = new List<ItemCart>();

            }
            else
            {
                cart = (List<ItemCart>)HttpContext.Session["yourcart"];
            }


            // cal total of your cart

            float total = 0;

            foreach (ItemCart it in cart)
            {

                total += it.LineTotal;
            }

            ViewBag.Total = total;
            //passing to View
            return View(cart);
        }
        [HttpPost]
        public ActionResult AddToCart()
        {


            //step 1
            List<ItemCart> cart = null;
            if (HttpContext.Session["yourcart"] == null)
            {
                cart = new List<ItemCart>();

            }
            else
            {
                cart = (List<ItemCart>)HttpContext.Session["yourcart"];
            }



            int Id = int.Parse(Request.Form["Id"]);

            //ItemCart 
            Product product = ctx.Products.Where(t => t.Id == Id).SingleOrDefault();
            int qty = Convert.ToInt32(Request.Form["txtQuantity"]);

            ItemCart item = new ItemCart()
            {

                Producti = product,
                Quantity = qty,
                LineTotal = (float)(qty * product.Price)

            };
            //step 2
            cart.Add(item);
            //step 3

            HttpContext.Session["yourcart"] = cart;



            return RedirectToAction("Cart");
        }
        public ActionResult DeleteItemCart(int id)
        {
            List<ItemCart> cart = (List<ItemCart>)HttpContext.Session["yourcart"];
            //Product product = ctx.Products.Where(t => t.Id == id).SingleOrDefault();
            ItemCart item = cart.Where(c=> c.Producti.Id==id).FirstOrDefault();
            
                cart.Remove(item);
                return RedirectToAction("Cart");
            
            //

            //HttpContext.Session["yourcart"] = cart;

            //xoa
            
          
           
        }
        public ActionResult Payment()
        {
            if(Session["CustomerId"] != null)
            {
                
                List<ItemCart> cart = null;
                if (HttpContext.Session["yourcart"] == null)
                {
                    cart = new List<ItemCart>();

                }
                else
                {
                    cart = (List<ItemCart>)HttpContext.Session["yourcart"];
                }


                // cal total of your cart

                float total = 0;

                foreach (ItemCart it in cart)
                {

                    total += it.LineTotal;
                }

                ViewBag.Total = total;

                Bill bill = new Bill()
                {
                    IdCustomer = Convert.ToInt32(Session["CustomerId"]),
                    IdEmloyee = 2,
                    Status = 1,
                    Date_Created = DateTime.Now,
                    Total = (decimal?)total
                };
                ctx.Bills.Add(bill);
                ctx.SaveChanges();

                foreach (ItemCart it in cart)
                {


                    Bill_details bill_Details = new Bill_details()
                    {
                        IdBill = bill.IdBill,///
                        IdProduct = it.Producti.Id,
                        Quality = it.Quantity,
                        Price = (decimal?)it.LineTotal
                    };
                    ctx.Bill_details.Add(bill_Details);
                    ctx.SaveChanges();

                }
                Session.Remove("yourcart");
                return RedirectToAction("Products");
            }
            else
            {
                return View("Login");
            }

        }
    }
}
using CanonStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanonStore.Controllers
{
    public class AdminController : Controller
    {
        
        Canon_StoreEntities4 ctx = new Canon_StoreEntities4();
        // GET: Admin
        //----------Products----------
        public ActionResult Index()
        {
            return View("Login");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(EmloyeeCheck emloyeeCheck)
        {
            if (ModelState.IsValid)
            {
                using (Canon_StoreEntities4 db = new Canon_StoreEntities4())
                {
                    var obj = db.Emloyees.Where(a => a.UserName == emloyeeCheck.UserName && a.Password == emloyeeCheck.Password).FirstOrDefault();

                    if (obj != null)
                    {
                        Session["EmloyeeId"] = obj.Id.ToString();
                        Session["EmloyeeUserName"] = obj.UserName.ToString();
                        Session["EmloyeeName"] = obj.Name.ToString();
                        //ViewBag.EmloyeeName = (Emloyees.Name)HttpContext.Session["EmloyeeId"];
                        //ViewBag.EmloyeeName = HttpContext.Session.SessionID;
                        return RedirectToAction("Products");
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

        public ActionResult Products()
        {
            if (Session["EmloyeeId"] != null)
            {
                List<Product> products = ctx.Products.ToList();
                return View(products);
            }
            else
            {
                return View("Login");
            }
            

           
        }
        public ActionResult ProductDetails(int id)
        {
            if (Session["EmloyeeId"] != null)
            {
                Product product = ctx.Products.Where(c => c.Id == id).SingleOrDefault();
                return View(product);
            }
            else
            {
                return View("Login");
            }

        }
        public ActionResult ProductAdd()
        {
            if (Session["EmloyeeId"] != null)
            {
                Product product = new Product();

                List<Type_Product> type_Products = ctx.Type_Product.ToList();
                ViewBag.type_Products = type_Products;

                List<Mount_Product> mount_Products = ctx.Mount_Product.ToList();
                ViewBag.mount_Products = mount_Products;

                return View(product);
            }
            else
            {
                return View("Login");
            }

        }
        [HttpGet]
        public ActionResult ProductEdit(int id)
        {
            if (Session["EmloyeeId"] != null)
            {
                List<Type_Product> type_Products = ctx.Type_Product.ToList();
                ViewBag.type_Products = type_Products;

                List<Mount_Product> mount_Products = ctx.Mount_Product.ToList();
                ViewBag.mount_Products = mount_Products;

                Product product = ctx.Products.Where(c => c.Id == id).SingleOrDefault();
                ViewBag.productId = id;
                //passing data / model to view
                return View(product);
            }
            else
            {
                return View("Login");
            }


        }
        [HttpPost]
        public ActionResult ProductSave(Product product)
        {
            if (Session["EmloyeeId"] != null)
            {
                if (ModelState.IsValid)
                {

                    ctx.Products.Add(product);
                    ctx.SaveChanges();

                    return RedirectToAction("Products");
                }
                else
                {
                    List<Type_Product> type_Products = ctx.Type_Product.ToList();
                    ViewBag.type_Products = type_Products;

                    List<Mount_Product> mount_Products = ctx.Mount_Product.ToList();
                    ViewBag.mount_Products = mount_Products;

                    return View("ProductAdd");
                }
            }
            else
            {
                return View("Login");
            }


        }
        [HttpPost]
        public ActionResult ProductUpdate(Product product)
        {
            if (Session["EmloyeeId"] != null)
            {
                //search old entity
                Product oldProduct = ctx.Products.Where(c => c.Id == product.Id).SingleOrDefault();

                oldProduct.Name = product.Name;
                oldProduct.Brand = product.Brand;
                oldProduct.Price = product.Price;
                oldProduct.Type = product.Type;
                oldProduct.Mount = product.Mount;
                oldProduct.Warranty = product.Warranty;
                oldProduct.Description = product.Description;
                oldProduct.Image = product.Image;
                ctx.SaveChanges();

                return RedirectToAction("Products");
            }
            else
            {
                return View("Login");
            }

        }
        public ActionResult ProductDelete(int id)
        {
            if (Session["EmloyeeId"] != null)
            {
                Product product = ctx.Products.Where(c => c.Id == id).SingleOrDefault();
                //xoa
                ctx.Products.Remove(product);
                ctx.SaveChanges();
                //redirect view
                return RedirectToAction("Products");

            }
            else
            {
                return View("Login");
            }

        }
        //----------Customers----------
        public ActionResult Customers()
        {
            if (Session["EmloyeeId"] != null)
            {
                List<Customer> customers = ctx.Customers.ToList();
                return View(customers);
            }
            else
            {
                return View("Login");
            }

        }
        public ActionResult CustomerDetail(int id)
        {
            if (Session["EmloyeeId"] != null)
            {
                Customer customer = ctx.Customers.Where(c => c.id == id).SingleOrDefault();

                List<Bill> bills = ctx.Bills.Where(c => c.IdCustomer == id).ToList();

                ViewBag.bills = bills;

                return View(customer);
            }
            else
            {
                return View("Login");
            }

        }
        [HttpGet]
        public ActionResult CustomerEdit(int id)
        {
            if (Session["EmloyeeId"] != null)
            {
                Customer customer = ctx.Customers.Where(c => c.id == id).SingleOrDefault();
                ViewBag.customerId = id;
                //passing data / model to view
                return View(customer);
            }
            else
            {
                return View("Login");
            }

        }
        public ActionResult CustomerDelete(int id)
        {
            if (Session["EmloyeeId"] != null)
            {
                Customer customer = ctx.Customers.Where(c => c.id == id).SingleOrDefault();


                //xoa
                ctx.Customers.Remove(customer);
                ctx.SaveChanges();
                //redirect view
                return RedirectToAction("Customers");
            }
            else
            {
                return View("Login");
            }

        }
        [HttpPost]
        public ActionResult CustomerUpdate(Customer customer)
        {
            if (Session["EmloyeeId"] != null)
            {
                //search old entity
                Customer oldCustomer = ctx.Customers.Where(c => c.id == customer.id).SingleOrDefault();

                oldCustomer.Name = customer.Name;
                oldCustomer.Address = customer.Address;
                oldCustomer.Phone = customer.Phone;
                //oldCustomer.DayOfBirth = customer.DayOfBirth;


                ctx.SaveChanges();

                return RedirectToAction("Customers");
            }
            else
            {
                return View("Login");
            }

        }

        //----------Emloyees----------
        public ActionResult Emloyees()
        {
            if (Session["EmloyeeId"] != null)
            {
                List<Emloyee> emloyees = ctx.Emloyees.ToList();
                return View(emloyees);
            }

            return View("Login");
           
        }
        public ActionResult EmloyeeAdd()
        {
            if (Session["EmloyeeId"] != null)
            {
                Emloyee emloyee = new Emloyee();

                return View(emloyee);
            }
            else
            {
                return View("Login");
            }

        }
        [HttpGet]
        public ActionResult EmloyeeEdit(int id)
        {
            if (Session["EmloyeeId"] != null)
            {
                Emloyee emloyee = ctx.Emloyees.Where(c => c.Id == id).SingleOrDefault();
                ViewBag.emloyeeId = id;
                //passing data / model to view
                return View(emloyee);
            }
            else
            {
                return View("Login");
            }

        }
        public ActionResult EmloyeeDelete(int id)
        {
            if (Session["EmloyeeId"] != null)
            {
                Emloyee emloyee = ctx.Emloyees.Where(c => c.Id == id).SingleOrDefault();


                //xoa
                ctx.Emloyees.Remove(emloyee);
                ctx.SaveChanges();
                //redirect view
                return RedirectToAction("Emloyees");
            }
            else
            {
                return View("Login");
            }

        }
        [HttpPost]
        public ActionResult EmloyeeSave(Emloyee emloyee)
        {
            if (Session["EmloyeeId"] != null)
            {
                if (ModelState.IsValid)
                {
                    ctx.Emloyees.Add(emloyee);
                    ctx.SaveChanges();
                    return RedirectToAction("Emloyees");
                }
                else
                {
                    return View("EmloyeeAdd");
                }
            }
            else
            {
                return View("Login");
            }




        }
        [HttpPost]
        public ActionResult EmloyeeUpdate(Emloyee emloyee)
        {
            if (Session["EmloyeeId"] != null)
            {//search old entity
                Emloyee oldEmloyee = ctx.Emloyees.Where(c => c.Id == emloyee.Id).SingleOrDefault();

                oldEmloyee.Name = emloyee.Name;
                oldEmloyee.Phone = emloyee.Phone;

                ctx.SaveChanges();

                return RedirectToAction("Emloyees");

            }
            else
            {
                return View("Login");
            }

        }

        //----------Bill----------
        public ActionResult Bills()
        {
            if (Session["EmloyeeId"] != null)
            {
                
                List<Bill> bills = ctx.Bills.ToList();

                List<Bill_Status> bill_Statuses = ctx.Bill_Status.ToList();
                ViewBag.bill_Statuses = bill_Statuses;
                return View(bills);
            }
            else
            {
                return View("Login");
            }

        }
        [HttpGet]
        public ActionResult BillEdit(int id)
        {
            if (Session["EmloyeeId"] != null)
            {
                List<Customer> customers = ctx.Customers.ToList();
                ViewBag.customers = customers;

                List<Emloyee> emloyees = ctx.Emloyees.ToList();
                ViewBag.emloyees = emloyees;

                List<Bill_Status> bill_Statuses = ctx.Bill_Status.ToList();
                ViewBag.bill_Statuses = bill_Statuses;

                Bill bill = ctx.Bills.Where(c => c.IdBill == id).SingleOrDefault();
                return View(bill);
            }
            else
            {
                return View("Login");
            }

        }
        [HttpPost]
        public ActionResult BillUpdate(Bill bill)
        {
            if (Session["EmloyeeId"] != null)
            {
                Bill oldBill = ctx.Bills.Where(c => c.IdBill == bill.IdBill).SingleOrDefault();


                oldBill.IdCustomer = bill.IdCustomer;

                oldBill.IdEmloyee = bill.IdEmloyee;
                oldBill.Status = bill.Status;
                oldBill.Total = bill.Total;
                oldBill.Date_Created = bill.Date_Created;
                ctx.SaveChanges();
                return RedirectToAction("Bills");
            }
            else
            {
                return View("Login");
            };
           
        }
        public ActionResult BillDetail(int id)
        {
            if (Session["EmloyeeId"] != null)
            {
                Bill bill = ctx.Bills.Where(c => c.IdBill == id).SingleOrDefault();

                List<Bill_details> bill_Details = ctx.Bill_details.Where(c => c.IdBill == id).ToList();

                ViewBag.bill_Details = bill_Details;

                Customer customer = ctx.Customers.Where(c => c.id == bill.IdCustomer).SingleOrDefault();
                ViewBag.customer = customer;


                Emloyee emloyee = ctx.Emloyees.Where(c => c.Id == bill.IdEmloyee).SingleOrDefault();
                ViewBag.emloyee = emloyee;

                return View(bill);
            }
            else
            {
                return View("Login");
            }

            
            
        }
        public ActionResult BillDelete(int id)
        {
            if (Session["EmloyeeId"] != null)
            {
                Bill bill = ctx.Bills.Where(c => c.IdBill == id).SingleOrDefault();


                //xoa
                ctx.Bills.Remove(bill);
                ctx.SaveChanges();
                //redirect view
                return RedirectToAction("Bills");
            }
            else
            {
                return View("Login");
            }

            
           
        }
    }
}
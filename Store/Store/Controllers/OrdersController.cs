using Store.Models;
using Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
{
    public class OrdersController : Controller
    {
        StoreContext db = new StoreContext();
        decimal subTotal = 0;


        public ActionResult NewOrder()
        {
            var orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Employee = new Employee();
            orderView.Products = new List<ProductOrder>();
            Session["orderView"] = orderView;
            

            var Clientlist = db.Customers.ToList();
            Clientlist.Add(new Customer { CustomerId = 0, Name = "Seleccione un Cliente.." });
            Clientlist = Clientlist.OrderBy(c => c.LastName).ToList();
            ViewBag.CustomerId = new SelectList(Clientlist, "CustomerId", "FullName");


            var EmployeeList = db.Employees.ToList();
            EmployeeList.Add(new Employee { EmployeeId = 0, Name = "Seleccione un Empleado.." });
            EmployeeList = EmployeeList.OrderBy(c => c.LastName).ToList();
            ViewBag.EmployeeId = new SelectList(EmployeeList, "EmployeeId", "FullName");

            //decimal totalAmount;
           
             

            return View(orderView);
        }

        [HttpPost]
        public ActionResult NewOrder(OrderView orderView)
        {
            orderView = Session["orderView"] as OrderView;

            var customerId = int.Parse(Request["CustomerId"]);
            var EmployeeId = int.Parse(Request["EmployeeId"]);

            if (customerId == 0)
            {
                var Clientlist = db.Customers.ToList();
                Clientlist.Add(new Customer { CustomerId = 0, Name = "Seleccione un Producto.." });
                Clientlist = Clientlist.OrderBy(c => c.Name).ToList();

                ViewBag.ProductId = new SelectList(Clientlist, "CustomerId", "Name");
                ViewBag.Error = "Debe seleccionar un Cliente.";

                var EmployeeList = db.Employees.ToList();
                EmployeeList.Add(new Employee { EmployeeId = 0, Name = "Seleccione un Producto.." });
                EmployeeList = EmployeeList.OrderBy(c => c.Name).ToList();

                ViewBag.ProductId = new SelectList(Clientlist, "CustomerId", "Name");
                ViewBag.Error = "Debe seleccionar un Cliente.";

                return View(orderView);
            }

            var client = db.Customers.Find(customerId);
            if (client == null)
            {
                var Clientlist = db.Customers.ToList();
                Clientlist.Add(new Customer { CustomerId = 0, Name = "Seleccione un Producto.." });
                Clientlist = Clientlist.OrderBy(c => c.Name).ToList();

                ViewBag.ProductId = new SelectList(Clientlist, "CustomerId", "Name");
                

                var EmployeeList = db.Employees.ToList();
                EmployeeList.Add(new Employee { EmployeeId = 0, Name = "Seleccione un Producto.." });
                EmployeeList = EmployeeList.OrderBy(c => c.Name).ToList();

                ViewBag.ProductId = new SelectList(Clientlist, "CustomerId", "Name");
                ViewBag.Error = "Debe seleccionar un Cliente.";

                return View(orderView);
            }

            if (orderView.Products.Count == 0) 
            {
                var Clientlist = db.Customers.ToList();
                Clientlist.Add(new Customer { CustomerId = 0, Name = "Seleccione un Producto.." });
                Clientlist = Clientlist.OrderBy(c => c.Name).ToList();

                ViewBag.ProductId = new SelectList(Clientlist, "CustomerId", "Name");
                

                var EmployeeList = db.Employees.ToList();
                EmployeeList.Add(new Employee { EmployeeId = 0, Name = "Seleccione un Producto.." });
                EmployeeList = EmployeeList.OrderBy(c => c.Name).ToList();

                ViewBag.ProductId = new SelectList(Clientlist, "CustomerId", "Name");
                ViewBag.Error = "Debe Cargar un detalle.";

                return View(orderView);
            }

            decimal totalAmount = 0;
            foreach (var item in orderView.Products)
            {
                totalAmount += (item.UnitPrice * (decimal)item.Quantity);
                
            }
            
            var order = new Order
            {
                CustomerId = customerId,
                EmployeeId = EmployeeId,
                DateOrder = DateTime.Now,
                TotalAmount = totalAmount

            };

            db.Orders.Add(order);
            db.SaveChanges();

            var orderId = db.Orders.ToList().Select(o => o.OrderId).Max();

            foreach (var item in orderView.Products)
            {
                var ordertDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    Name = item.Name,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    OrderId = orderId

                };
                db.OrderDetails.Add(ordertDetail);
                db.SaveChanges();
            }
            
            ViewBag.Message = string.Format("La venta {0} fue generada correctamente", orderId);
            //RedirectToAction("NewOrder");

            var Clientlist_ = db.Customers.ToList();
            Clientlist_.Add(new Customer { CustomerId = 0, Name = "Seleccione un Cliente.." });
            Clientlist_ = Clientlist_.OrderBy(c => c.LastName).ToList();
            ViewBag.CustomerId = new SelectList(Clientlist_, "CustomerId", "FullName");


            var EmployeeList_ = db.Employees.ToList();
            EmployeeList_.Add(new Employee { EmployeeId = 0, Name = "Seleccione un Empleado.." });
            EmployeeList_ = EmployeeList_.OrderBy(c => c.LastName).ToList();
            ViewBag.EmployeeId = new SelectList(EmployeeList_, "EmployeeId", "FullName");

            //blanqueo la vista
            orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Employee = new Employee();
            orderView.Products = new List<ProductOrder>();
            Session["orderView"] = orderView;
            return View(orderView);
        }

        //GET
        public ActionResult AddProduct() 
        {

            var productList = db.Products.ToList();
            productList.Add(new ProductOrder { ProductId = 0, Name = "Seleccione un Producto.." });
            productList = productList.OrderBy(c => c.Name).ToList();

            ViewBag.ProductId = new SelectList(productList, "ProductId", "Name");

            return View();
        }

        
        [HttpPost]
        public ActionResult AddProduct(ProductOrder productOrder)
        {
            var orderView = Session["orderView"] as OrderView;

            var productId = int.Parse(Request["ProductId"]);

            if(productId == 0) 
            {
                var productList = db.Products.ToList();
                productList.Add(new ProductOrder { ProductId = 0, Name = "Seleccione un Producto.." });
                productList = productList.OrderBy(c => c.Name).ToList();

                ViewBag.ProductId = new SelectList(productList, "ProductId", "Name");
                ViewBag.Error = "Debe seleccionar un producto.";

                return View(productOrder);
            }

            var product = db.Products.Find(productId);
            if(product == null) 
            {
                var productList = db.Products.ToList();
                productList.Add(new ProductOrder { ProductId = 0, Name = "Seleccione un Producto.." });
                productList = productList.OrderBy(c => c.Name).ToList();

                ViewBag.ProductId = new SelectList(productList, "ProductId", "Name");
                ViewBag.Error = "El Producto no existe.";

                return View(productOrder);
            }



            productOrder = orderView.Products.Find(p => p.ProductId == productId);
            if(productOrder == null)
            {
                productOrder = new ProductOrder
                {
                    Name = product.Name,
                    UnitPrice = product.UnitPrice,
                    Quantity = float.Parse(Request["Quantity"]),
                    ProductId = product.ProductId
                };
                orderView.Products.Add(productOrder);
            }
            else 
            {
                productOrder.Quantity += float.Parse(Request["Quantity"]);
            }
           

            var Clientlist = db.Customers.ToList();
            Clientlist.Add(new Customer { CustomerId = 0, Name = "Seleccione un Cliente.." });
            Clientlist = Clientlist.OrderBy(c => c.LastName).ToList();
            ViewBag.CustomerId = new SelectList(Clientlist, "CustomerId", "FullName");


            var EmployeeList = db.Employees.ToList();
            EmployeeList.Add(new Employee { EmployeeId = 0, Name = "Seleccione un Empleado.." });
            EmployeeList = EmployeeList.OrderBy(c => c.LastName).ToList();
            ViewBag.EmployeeId = new SelectList(EmployeeList, "EmployeeId", "FullName");

            

            foreach (var item in orderView.Products)
            {
                 subTotal += (item.UnitPrice * (decimal)item.Quantity);
                ViewBag.Total = "$ " + subTotal;

            }

            return View("NewOrder", orderView);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
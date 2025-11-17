using Microsoft.AspNetCore.Mvc;
using MVCProject.Filters;
using MVCProject.Models;
using NuGet.Packaging;
using static System.Net.Mime.MediaTypeNames;


namespace MVCProject.Controllers
{
    [SessionAuthorize]
    public class HomePageController : Controller
    {
        Context context = new Context();
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            
            ViewBag.Category= context.Categories.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Product product,IFormFile Pic)
        {
           
            if (Pic != null && Pic.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedImages");


                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);


                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Pic.FileName);


                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Pic.CopyTo(fileStream);
                }


                product.Image = uniqueFileName;
            }
            //System.Diagnostics.Debug.WriteLine(product.ToString());
            product.QRCode = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
            context.Products.Add(product);

            context.SaveChanges();
            return RedirectToAction("AddProduct");
        }

        [HttpGet]
        public IActionResult AddCustomer() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            //System.Diagnostics.Debug.WriteLine(customer.ToString());
            context.Customers.Add(customer);
            context.SaveChanges();
            return View();
        }

        public IActionResult SelectCustomer()
        {
            List<Customer> customers = context.Customers.ToList();

            return View(customers);
        }
        public IActionResult SelectProducts(int ID)
        {


            ViewBag.Categort = context.Categories.ToList();
            List<Product> products = context.Products.ToList();
            TempData["CID"] = ID;
            return View(products);
        }
        public class ProductSelection
        {
            public int ID { get; set; }
            public int Quantity { get; set; }
        }

        public IActionResult InvoiceShow(List<ProductSelection> SelectedProducts)
        {
            int CID = Convert.ToInt32(TempData["CID"]);

            if (SelectedProducts.Count > 0)
            {
                Invoice invoice = new Invoice() { CustomerID=CID };
                context.Invoices.Add(invoice);
                context.SaveChanges();

                int InvID = invoice.InvoiceID;

                List<InvoiceDetail> invoiceDetails = new List<InvoiceDetail>();
                int TotalPrice = 0;
                foreach (var P in SelectedProducts)
                {
                    Product product = context.Products.FirstOrDefault(p => p.ID == P.ID);
                    product.Quantity -= P.Quantity;

                    invoiceDetails.Add(new InvoiceDetail { InvoiceID = InvID, Quantity = P.Quantity, ProductID = P.ID, Price = product.Price });
                    TotalPrice+= (product.Price* P.Quantity);
                }
                context.InvoiceDetails.AddRange(invoiceDetails);
                invoice.TotalAmount = TotalPrice;
                context.SaveChanges();
                ViewBag.C = CID;
                return RedirectToAction("InvoiceDetails", "Dashboard", new { InvoiceID = InvID });
            }
            return RedirectToAction("SelectProducts", new { ID = CID });


        }


       


    }
}

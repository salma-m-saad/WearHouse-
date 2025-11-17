using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Filters;
using MVCProject.Models;
namespace MVCProject.Controllers
{
    [SessionAuthorize]
    [AdminAuthorize]
    public class DashboardController : Controller
    {
        Context context = new Context();
        public IActionResult Charts()
        {
          
            var data = context.Invoices
                .GroupBy(i => i.Date.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Profit = g.Sum(i => i.TotalAmount)
                })
                .OrderBy(g => g.Date)
                .ToList();

            var chartDates = data.Select(d => d.Date.ToString("MMM dd")).ToList();
            var chartProfits = data.Select(d => d.Profit).ToList();

            ViewBag.ChartDates = chartDates;
            ViewBag.ChartProfits = chartProfits;

            return View();
        }


        public IActionResult CustomersDetails()
        {
            List<Customer> customers = context.Customers.ToList();

            return View(customers); 
        }
        public IActionResult EditCustomer(Customer customer) 
        {
            Customer customer1 = context.Customers.FirstOrDefault(c => c.ID == customer.ID);

            customer1.FirstName = customer.FirstName;
            customer1.LastName = customer.LastName;
            customer1.Address = customer.Address;
            customer1.Phone = customer1.Phone;
            customer1.NationalID = customer.NationalID;
            customer1.IsRedFlag = customer.IsRedFlag;


            
            context.SaveChanges();
            TempData["Message"] = "Customer updated successfully";
            return RedirectToAction("CustomersDetails");
        }
        public IActionResult DeleteCustomer(int ID)
        {
            Customer customer1 = context.Customers.FirstOrDefault(c => c.ID == ID);

            context.Customers.Remove(customer1);
            

            context.SaveChanges();
            TempData["Message"] = "Customer deleted successfully";
            return RedirectToAction("CustomersDetails");
        }

        public IActionResult ProductsDetails()
        {
            ViewBag.Categort =context.Categories.ToList();
            List<Product> products =context.Products.ToList();

            return View(products);
        }
        public IActionResult EditProduct(Product pro)
        {
            Product product = context.Products.FirstOrDefault(p => p.ID == pro.ID);
            product.Description = pro.Description;
            product.Price = pro.Price;
            product.Quantity = pro.Quantity;
            product.Size = pro.Size;
            product.CategoryID = pro.CategoryID;
            

            context.SaveChanges();
            TempData["Message"] = " Product updated successfully";
            return RedirectToAction("ProductsDetails");
        }
        public IActionResult DeleteProduct(int ID)
        {
            Product product = context.Products.FirstOrDefault(p => p.ID == ID);

            context.Products.Remove(product);


            context.SaveChanges();
            TempData["Message"] = "Product deleted successfully";
            return RedirectToAction("ProductsDetails");
        }

        public IActionResult Invoices()
        {
            
            List<Invoice> invoices = context.Invoices.ToList();

            return View(invoices);
        }
        public IActionResult InvoiceDetails(int InvoiceID)
        {

            ViewBag.Category = context.Categories.ToList();

            Invoice invoice = context.Invoices
                .Include(i => i.Customer) 
                .Include(i => i.InvoiceDetails)
                    .ThenInclude(d => d.Product) 
                .Include(i => i.Payment) 
                .FirstOrDefault(i => i.InvoiceID == InvoiceID);

            invoice.InvoiceDetails = invoice.InvoiceDetails
            .GroupBy(d => d.ProductID)
            .Select(g => g.First())
            .ToList();
            return View(invoice);
        }

        public IActionResult EditInvoice(int InvoiceID,List<int> SelectedProducts) 
        {

            Invoice invoice = context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.InvoiceDetails)
                    .ThenInclude(d => d.Product)
                .FirstOrDefault(i => i.InvoiceID == InvoiceID);

            invoice.InvoiceDetails = invoice.InvoiceDetails
            .GroupBy(d => d.ProductID)
            .Select(g => g.First())
            .ToList();

            foreach (var InvDetailID in SelectedProducts) 
            {

                InvoiceDetail invoiceDetail=invoice.InvoiceDetails.FirstOrDefault(d => d.InvoiceDetailID == InvDetailID);

                invoiceDetail.Product.Quantity += invoiceDetail.Quantity;

                invoice.TotalAmount -= (invoiceDetail.Quantity * invoiceDetail.Price);

                context.Remove(invoiceDetail);

            }
            Invoice invoice2 = context.Invoices.Include(i => i.InvoiceDetails).FirstOrDefault(i => i.InvoiceID == InvoiceID);
            if (invoice2.InvoiceDetails.Count <= 0) 
            {
                context.Remove(invoice2);
            }
            
            context.SaveChanges();
           
            return RedirectToAction("Invoices");
        }
        public IActionResult DeleteInvoice(int InvoiceID) 
        {
            
            Invoice invoice = context.Invoices.Include(i=>i.InvoiceDetails).FirstOrDefault(i => i.InvoiceID == InvoiceID);
            if (invoice != null)
            {
                if (invoice.InvoiceDetails != null) context.InvoiceDetails.RemoveRange(invoice.InvoiceDetails);
                context.Invoices.Remove(invoice);

            }
            

          
            context.SaveChanges();
            return RedirectToAction("Invoices");
        }

    }
}

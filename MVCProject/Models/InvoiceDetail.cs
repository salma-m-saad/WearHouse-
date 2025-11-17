using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models
{
    public class InvoiceDetail
    {
        public int InvoiceDetailID { get; set; }

        public int InvoiceID { get; set; }
        public virtual Invoice Invoice { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }


        public int Price { get; set; }

        public bool IsReturned { get; set; } = false;

        // Property not mapped to DB (calculated)
        public int Subtotal => Quantity * Price;
    }
}

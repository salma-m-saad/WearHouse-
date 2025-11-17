using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }

        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        
        public int TotalAmount { get; set; }

        [MaxLength(200)]
        public string? Notes { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual Payment Payment { get; set; }
    }
}

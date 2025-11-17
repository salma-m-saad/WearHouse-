using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }

        public int InvoiceID { get; set; }
        public virtual Invoice Invoice { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public int Amount { get; set; }

        [MaxLength(20)]
        public string Method { get; set; } = "Cash";
    }
}

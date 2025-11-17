using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int  CategoryID  {get;set;}
        [MaxLength(10)]
        public string Size { get; set; }

      
        public int Price { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Description { get; set; }

        public string? Image { get; set; }

        public string QRCode { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }

        public override string ToString()
        {
            return $"ID is {ID},  CategoryID is{CategoryID} , size is {Size} ,Price is {Price},  Quantity is{Quantity} , Description is {Description} , Image is {Image}";
        }
    }
}

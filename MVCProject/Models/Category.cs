using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        ICollection<Product> Products { get; set; }
    }
}

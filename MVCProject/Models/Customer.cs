using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? FirstName { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? LastName { get; set; }
        public string Phone { get; set; }
        public string? NationalID { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsRedFlag { get; set; } = false;

        public override string ToString()
        {
            return $"FN {FirstName} , LN {LastName} , Ph {Phone} , NID {NationalID} , add {Address} , redf {IsRedFlag}" ;
        }
    }
 
}

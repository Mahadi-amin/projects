using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Entities
{
    public class PurchaseInformation
    {
        [Key]
        public int PurchaseId { get; set; }
        public string SellerName { get; set; }
        public string SellerContactInfo { get; set; }
        public string TypeOfPet { get; set; }
        public double PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}

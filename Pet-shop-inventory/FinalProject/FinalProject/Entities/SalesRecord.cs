using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Entities
{
    public class SalesRecord
    {
        [Key]
        public int SalesId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContactInfo { get; set; }
        public double SalePrice { get; set; }
        public DateTime SaleDate { get; set; }
        public int PetId { get; set; }
        public string Type { get; set; }
    }
}

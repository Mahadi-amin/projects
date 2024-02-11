using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Entities
{
    public class CageOrAquarium
    {
        [Key]
        public int CageOrAquariumId { get; set; }
        public string Type { get; set; }
        public ICollection<Pet> Pets { get; set; }
        public ICollection<FeedingSchedule> FeedingSchedules { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Entities
{
    public class FeedingSchedule
    {
        [Key]
        public int ScheduleId { get; set; }
        public int CageOrAquariumId { get; set; }
        public string FeedTime { get; set; }
        public CageOrAquarium CageOrAquarium { get; set; }
    }
}

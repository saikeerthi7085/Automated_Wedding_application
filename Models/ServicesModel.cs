using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Automated_Wedding_Application.Models
{
    public enum PlannerServices
    {
        Bouquets, Decorations, Catering, Makeup, Photography, Winery, MusicBand , Priest
    }
    public class ServicesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        [DisplayName("Cost(£)")]
        public decimal Cost { get; set; }

        public byte[] Serviceimage { get; set; }

        [NotMapped]
        public string Serviceimageurl { get; set; }

        public int PlannerModelPlannerId { get; set; }
    }
}

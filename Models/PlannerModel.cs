using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Automated_Wedding_Application.Models
{
    public class PlannerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlannerId { get; set; }

        [Required]

        public string PlannerName { get; set; }
        [Required]
        public string About { get; set; }

        [Required]
        public string Location { get; set; }

       

        public decimal ServicesCost { get; set; }

        public decimal Rating { get; set; } 

        public string ApplicationUserId { get; set; }

       
        public byte[] Plannerimage { get; set; }
        [NotMapped]
        public string Imageurl { get; set; }
        [NotMapped]
        public int Quantity { get; set; }
        [NotMapped]
        [Required]
        [DataType(DataType.Date)]
        public DateTime  DeliveryDate { get; set; }
        [NotMapped]
        public int ServiceId { get; set; }

        [NotMapped]
        public string flag { get; set; }


        [NotMapped]
        public ServicesModel ServicesModel { get; set; }
        [NotMapped]
        public CustomerCart customerCart { get; set; }
        public List<ServicesModel> services { get; set; }

       


    }
}

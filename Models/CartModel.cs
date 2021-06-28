using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Automated_Wedding_Application.Models
{
    public enum Deliverystatus
    {
        Placed, Recieved, InProcess, Delivered
    }
    public class CustomerCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }

        public string UserId { get; set; }

        public string PlannerId { get; set; }

        public string ServiceId { get; set; }
        public string ServiceName { get; set; }

        public int Quantity { get; set; }

        public double ServiceCost { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
      
        public DateTime DeliveryDate { get; set; }

        public string Checkout { get; set; }

        public string PaymentStatus { get; set; }

        public string Deliverystatus { get; set; }

        public string Chargeid { get; set; }

        public string plannerpaymentid { get; set; }

        [NotMapped]
        public PlannerModel planner { get; set; }

    }
}

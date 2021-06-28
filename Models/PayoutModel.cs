using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Automated_Wedding_Application.Models
{
    public class PayoutModel
    {
       public string plannerid { get; set; }

        public string Stripaccountno { get; set; }
        public double Amount { get; set; }

        public string plannerpaymentid { get; set; }
    }
}

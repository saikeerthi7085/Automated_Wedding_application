using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Automated_Wedding_Application.Models;

namespace Automated_Wedding_Application.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        //[Required]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Only Alphabets are allowed.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "User Type")]

        public string UserType { get; set; }

        public List<PlannerModel> PlannerModels { get; set; }

        public string userstripeId { get; set; }

    }
}

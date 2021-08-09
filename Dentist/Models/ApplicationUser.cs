using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dentist.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Firstname can not be empty!")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname can not be empty!")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Employee Number")]
        [Required(ErrorMessage = "Employee Number can not be empty!")]
        [MinLength(5, ErrorMessage = "Employee Number needs to have a length of 5!")]
        [MaxLength(5, ErrorMessage = "Employee Number needs to have a length of 5!")]
        public int EmployeeNumber { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DentistBilling.Models
{
    public class Costumer
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Firstname can not be empty!")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname can not be empty!")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "CPR number")]
        [Required(ErrorMessage = "CPR number can not be empty!")]
        [MinLength(10, ErrorMessage = "CPR number needs to have a length of 10!")]
        [MaxLength(10, ErrorMessage = "CPR number needs to have a length of 10!")]
        public string CPRNumber { get; set; }

        [Display(Name = "Street")]
        [Required(ErrorMessage = "Street can not be empty!")]
        public string StreetName { get; set; }

        [Display(Name = "Street Number")]
        [Required(ErrorMessage = "Street Number can not be empty!")]
        public string StreetNumber { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Zip Code can not be empty!")]
        public string ZipCode { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City can not be empty!")]
        public string City { get; set; }

        [Required(ErrorMessage = "Insurance need to be set!")]
        public bool Insured { get; set; }

        [Display(Name = "Bills")]
        public ICollection<Bill> Bills { get; } = new ObservableCollection<Bill>();
    }
}

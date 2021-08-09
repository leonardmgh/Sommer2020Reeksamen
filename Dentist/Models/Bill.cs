using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dentist.Models
{
    public class Bill
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Description can not be empty!")]
        [Display(Name = "Billing Date")]
        public DateTimeOffset BillDate { get; set; }

        [Required(ErrorMessage = "Items can not be empty!")]
        [Display(Name = "Items")]
        public ICollection<BillableItems> Items { get; set; }

        [Required(ErrorMessage = "Costumer can not be empty!")]
        [Display(Name = "Costumer")]
        public Costumer Costumer { get; set; }

        [Required(ErrorMessage = "Bill maker can not be empty!")]
        [Display(Name = "Bill maker")]
        public ApplicationUser BillMaker { get; set; }
    }
}

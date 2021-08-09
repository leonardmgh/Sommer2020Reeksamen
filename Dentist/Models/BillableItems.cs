using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dentist.Models
{
    public class BillableItems
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Description can not be empty!")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "CostumerPart can not be empty!")]
        [Display(Name = "Costumer Part")]
        public float CostumerPart { get; set; }

        [Required(ErrorMessage = "Insurance Part can not be empty!")]
        [Display(Name = "Insurance Part")]
        public float InsurancePart { get; set; }

        public ICollection<Bill> Bills { get; set; }
    }
}

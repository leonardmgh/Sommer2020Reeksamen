using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DentistBilling.Models
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
        public double CostumerPart { get; set; }

        [Required(ErrorMessage = "Insurance Part can not be empty!")]
        [Display(Name = "Insurance Part")]
        public double InsurancePart { get; set; }

        public ICollection<BillToItem> Bills { get; } = new ObservableCollection<BillToItem>();
    }
}

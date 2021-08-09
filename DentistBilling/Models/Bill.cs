using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DentistBilling.Models
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
        public ICollection<BillToItem> Items { get; } = new ObservableCollection<BillToItem>();

        [ForeignKey("Costumer")]
        [Required]
        public int CostumerID { get; set; }

        [Required(ErrorMessage = "Costumer can not be empty!")]
        [Display(Name = "Costumer")]
        public Costumer Costumer { get; set; }

        [Display(Name = "Bill maker")]
        public ApplicationUser BillMaker { get; set; }

        [Required]
        [Display(Name = "Total Costumer")]
        public double TotalCostumer { get; set; }

        [Required]
        [Display(Name = "Total Insurance")]
        public double TotalInsureance { get; set; }
    }
}

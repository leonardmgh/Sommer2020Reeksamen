using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DentistBilling.Models
{
    public class BillToItem
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int BillID { get; set; }
        public Bill Bill { get; set; }

        [Required]
        public int Counter { get; set; }

        [Required]
        public int ItemID { get; set; }
        public BillableItems BillableItem { get; set; }
    }
}

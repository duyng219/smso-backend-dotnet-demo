using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class PaymentHistory
    {
        [Key]
        public int PaymentId { get; set; }
        public string PaymentMethod { get; set; }
        public float PaymentPrice { get; set; }
        public DateTime PaymentDate { get; set; }
        public int  ServiceId { get; set; }
        public int UserId { get; set; }

    }
}

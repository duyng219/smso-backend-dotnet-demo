using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models.JWT
{
    public class RefreshToken: EntityBase<int>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string ReffreshToken { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevorked { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpiryDate { get; set; }

     
    }
}

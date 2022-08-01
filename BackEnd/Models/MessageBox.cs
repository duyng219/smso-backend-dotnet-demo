using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class MessageBox
    {
        [Key]
        public int MessageId { get; set; }
        public int CountMessage { get; set; }
        public int FriendId { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        [StringLength(120)]
        public string Message { get; set; }
        public int UserId  { get; set; }
        

    }
}

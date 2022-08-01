using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    
    public class Friend
    {
        [Key]
        public int Id { get; set; }
        public bool FriendRequest { get; set; }
        public bool IsFriend { get; set; }
        public string ContactName { get; set; }
        public int FriendId { get; set; }
        public int UserId { get; set; }
    }
}

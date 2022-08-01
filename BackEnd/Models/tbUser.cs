using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class tbUser
    {
        [Key]
        
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public int Birthday { get; set; }
        public string Address { get; set; }
        public string MaritalStatus { get; set; }
        public string Hobbies { get; set; }
        public string Education { get; set; }
        public string Work { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public DateTime dateCreate { get; set; }
        public DateTime dateUpdate { get; set; }
        public string IsAdmin { get; set; }
    }
}

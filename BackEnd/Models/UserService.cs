using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class UserService
    {
        [Key]
        public int Id { get; set; }
        public int ServiceRegisterId { get; set; }
        public string ServiceRegisterName{ get; set; }
        public Boolean Service { get; set; }
        public int UserId { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class ServiceRegister
    {
        [Key]
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string description { get; set; }
        public string urlImage { get; set; }
        public string urlLinkService { get; set; }
        public float ServicePrice { get; set; }
        public int CategoryId { get; set; }
       
   
        //	ServiceId int primary key,
        //ServiceName varchar(200),
        //ServiceContent varchar(max),
        //ServicePrice float,
        //UserId int foreign key references tbUser(UserId),
        //CategoryId int foreign key references ServiceCategory(CategoryId)

    }
}

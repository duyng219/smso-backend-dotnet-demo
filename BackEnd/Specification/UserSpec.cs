using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using BackEnd.Models;

namespace BackEnd.Specification
{
    public class UserSpec : Specification<tbUser>, ISingleResultSpecification
    {
        public UserSpec(string email, string password)
        {
            Query.Where(item => item.Email == email && item.Password == password);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<tbUser> Users { get; set; }
        public DbSet<ServiceRegister> ServiceRegisters { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }
        public DbSet<MessageBox> MessageBoxes { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<UserService> UserServices { get; set; }

    }
}

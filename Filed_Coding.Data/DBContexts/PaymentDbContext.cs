using Filed_Coding.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filed_Coding.Data.DBContexts
{
   public class PaymentDbContext:DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<ApplicationUser>().ToTable("Users");
            //builder.Entity<RequestStatus>().HasData(new RequestStatus { Id = 1, Status = "Pending Approval" },
            //   new RequestStatus { Id = 2, Status = "Approved" },
            //   new RequestStatus { Id = 3, Status = "Rejected" }
            //  );
        }
        public DbSet<Payment> Payments { get; set; }
    }
}

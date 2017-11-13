using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CouponManagerAPI
{
    public class ApiContext : DbContext
    {
        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<Coupon> Coupon { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }
        
    }
}

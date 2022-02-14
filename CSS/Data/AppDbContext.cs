using CSS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSS.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<TaxPayer> TaxPayers { get; set; }
        public DbSet<Complain> Complains { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<ComplainType> ComplainTypes { get; set; }
        public DbSet<User> Users{ get; set; }

        internal Task ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}

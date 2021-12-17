using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Musical_Instrument_Store.Data.Models.UserAppDBContext
{
    public class UserAppDBContext : IdentityDbContext<User>
    {
        private readonly DbContextOptions _options;

        public UserAppDBContext(DbContextOptions<UserAppDBContext> options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

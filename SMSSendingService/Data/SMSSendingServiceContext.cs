using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SMSSendingService.Models;

namespace SMSSendingService.Data
{
    public class SMSSendingServiceContext : DbContext
    {
        public SMSSendingServiceContext (DbContextOptions<SMSSendingServiceContext> options)
            : base(options)
        {
        }

        public DbSet<SMSSendingService.Models.SMS> SMS { get; set; } = default!;
    }
}

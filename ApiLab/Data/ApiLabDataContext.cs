using ApiLab.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLab.Data
{
    public class ApiLabDataContext: DbContext
    {
        public ApiLabDataContext(DbContextOptions<ApiLabDataContext> options) : base(options) { }

        public DbSet<LabImage> LabImages { get; set; }    }
}

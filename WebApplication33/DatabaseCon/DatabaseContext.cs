using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication33.Models;

namespace WebApplication33.DatabaseCon
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Registration> Registration { get; set; }
        public DbSet<Books> Book { get; set; }
    }
}
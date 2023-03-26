using Castle.Core.Configuration;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext()
        {

        }
        IConfiguration _config;

        public MySqlContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseMySql("server = localhost; port = 3306; database = Todo; user = root; password = mehmet", ServerVersion.Create(8, 0, 32, ServerType.MySql)));

        }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Todo> Todos { get; set; }
    }
}

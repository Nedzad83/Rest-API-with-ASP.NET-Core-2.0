using Microsoft.EntityFrameworkCore;
using RestAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPI.Data
{
    public class EnvironmentContext : DbContext
    {
        public EnvironmentContext(DbContextOptions<EnvironmentContext> options) : base(options)
        {

        }

        public DbSet<EnvDeployment> Environments { get; set; }

        public DbSet<EnvDeployment> CurrentEnvironments { get; set; }

    }
}

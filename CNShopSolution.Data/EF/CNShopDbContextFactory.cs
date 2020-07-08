using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CNShopSolution.Data.EF
{
    class CNShopDbContextFactory : IDesignTimeDbContextFactory<CNShopDbContext>
    {
        public CNShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appseting.json")
             .Build();

            var connecttonString = configuration.GetConnectionString("CNShopSolution");
            var optionBulder = new DbContextOptionsBuilder<CNShopDbContext>();
            optionBulder.UseSqlServer(connecttonString);
            return new CNShopDbContext(optionBulder.Options);


        }
    }
}

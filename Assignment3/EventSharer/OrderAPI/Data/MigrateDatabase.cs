using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Data
{
    public class MigrateDatabase
    {
        public static void EnsureCreated(OrdersContext context)
        {
            System.Console.WriteLine("Creating database...");
            context.Database.EnsureCreated();
            RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
            databaseCreator.CreateTables();

            System.Console.WriteLine("Database and tables creation complete...");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microservice.Infrastructure;

namespace Microservice.Tests
{
    public class DbContextFactory
    {
        private readonly string _connectionString;

        public DbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AssociateDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AssociateDbContext>();
                options.UseSqlServer(_connectionString);
            return new AssociateDbContext(options.Options);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using sample_service.Entities.Sample;

namespace sample_service_test.Mock.Entities
{
    public partial class MockSampleDbContext : SampleDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }
        }
    }
}

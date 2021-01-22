using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_service.Entities.Sample
{
    public class DbInitializer
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            SampleDbContext context = applicationBuilder.ApplicationServices.GetRequiredService<SampleDbContext>();

            User user = new User
            { Id = 1, Firstname = "Kemal", Lastname="Akçıl", Email="kakcil@trabilisim.com", Password="123",CreateDate = DateTime.Now, Status=true };

            Country country = new Country
            { Id = 1, Name = "Türkiye" };

            City city = new City
            { Id = 1, Name = "Sakarya", CountryId = 1 };

            District district = new District
            { Id = 1, Name = "Adapazarı", CityId = 1 };

            context.User.Add(user);

            context.Country.Add(country);

            context.City.Add(city);

            context.District.Add(district);

            context.SaveChanges();
        }
    }
}

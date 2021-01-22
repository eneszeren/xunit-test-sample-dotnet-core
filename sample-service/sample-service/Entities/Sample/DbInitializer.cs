using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace sample_service.Entities.Sample
{
    public static class DataSeeder
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<SampleDbContext>();

                User user = new User
                { Id = 1, Firstname = "Kemal", Lastname = "Akçıl", Email = "kakcil@trabilisim.com", Password = "123", CreateDate = DateTime.Now, Status = true };

                Country country = new Country
                { Id = 1, Name = "Türkiye" };

                City city = new City
                { Id = 1, Name = "Sakarya", CountryId = 1 };

                District district = new District
                { Id = 1, Name = "Adapazarı", CityId = 1 };

                db.User.Add(user);

                db.Country.Add(country);

                db.City.Add(city);

                db.District.Add(district);

                db.SaveChanges();
            }
        }
    }
}

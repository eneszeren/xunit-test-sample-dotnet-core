using sample_service.Entities.Sample;
using sample_service.Helpers;
using sample_service.Services;
using sample_service_test.Mock.Entities;
using sample_service_test.Mock.Helper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace sample_service_test.Fixture
{
    public class ServiceFixture : IDisposable
    {

        public IOptions<AppSettings> appSettings = Options.Create<AppSettings>(new AppSettings()
        {
            Secret = "46C39F9CCDA995A64829D512DCA9F",
        });

        public MockSampleDbContext MockSampleDbContext { get; private set; }

        public MockHttpClient HttpClient { get; set; }

        public CityService CityService { get; set; }
        public CountryService CountryService { get; set; }
        public DistrictService DistrictService { get; set; }
        public UserService UserService { get; set; }



        public ServiceFixture()
        {

            //Mock

            //Helpers
            HttpClient = new MockHttpClient();

            // Entities
            MockSampleDbContext = new MockSampleDbContext();
            MockSampleDbContext.Database.EnsureDeleted();

            // Services
            CountryService = new CountryService(MockSampleDbContext);

            CityService = new CityService(MockSampleDbContext, CountryService);

            DistrictService = new DistrictService(MockSampleDbContext, CityService);

            LoadMockData();
        }

        private void LoadMockData()
        {
            MockSampleDbContext.Country.AddRange(new List<Country>()
            {
                new Country()
                {
                    Id = 1,
                    Name = "Türkiye",
                }
            });
            MockSampleDbContext.City.AddRange(new List<City>()
            {
                new City()
                {
                    Id = 1,
                    Name = "Sakarya",
                    CountryId = 1 //Türkiye
                }
            });
            MockSampleDbContext.District.AddRange(new List<District>()
            {
                new District()
                {
                    Id = 1,
                    Name = "Sakarya",
                    CityId = 1, //Sakarya
                }
            });

            MockSampleDbContext.SaveChanges();
        }

        #region ImplementIDisposableCorrectly
        /** https://docs.microsoft.com/en-us/visualstudio/code-quality/ca1063?view=vs-2019 */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't
        // own unmanaged resources, but leave the other methods
        // exactly as they are.
        ~ServiceFixture()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                MockSampleDbContext.Dispose();
            }
        }
        #endregion
    }
}

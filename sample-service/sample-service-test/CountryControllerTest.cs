using sample_service.Controllers;
using sample_service.Dtos;
using sample_service_test.Fixture;
using sample_service_test.Mock.Helper;
using sample_service_test.Theory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace sample_service_test
{
    public class CountryControllerTest : IClassFixture<ServiceFixture>
    {
        public CountryController CountryController { get; set; }

        public CountryControllerTest(ServiceFixture fixture)
        {
            CountryController = new CountryController(fixture.CountryService);
            CountryController.Authorize();
        }

        [Fact]
        public void ListCountry_WithNullData_ThanOkRequest()
        {
            OkObjectResult result = CountryController.List() as OkObjectResult;
            GeneralDto.Response res = (GeneralDto.Response)result.Value;
            List<CountryDto.Detail> data = res.Data as List<CountryDto.Detail>;
            Assert.True(data.Count > 0);
            Assert.False(res.Error);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Theory, ClassData(typeof(CountryControllerTheory.DetailCountryOkParams))]
        public void DetailCountry_WithData_ThanOkRequest(GeneralDto.DetailRequest detailRequest)
        {

            OkObjectResult result = CountryController.Detail(detailRequest) as OkObjectResult;
            GeneralDto.Response res = result.Value as GeneralDto.Response;
            Assert.True(res.Data != null);
            Assert.False(res.Error);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Theory, ClassData(typeof(CountryControllerTheory.DetailCountryBadParams))]
        public void DetailCountry_WithFalseData_ThanOkRequest(GeneralDto.DetailRequest detailRequest)
        {
            OkObjectResult result = CountryController.Detail(detailRequest) as OkObjectResult;
            GeneralDto.Response res = result.Value as GeneralDto.Response;
            Assert.True(res.Data == null);
            Assert.True(res.Message != null);
            Assert.True(res.Error);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        //update
        [Theory, ClassData(typeof(CountryControllerTheory.SaveCountryOkParams))]
        public void SaveCountry_WithId_ThanOKRequest(CountryDto.Save save)
        {
            OkObjectResult result = CountryController.Save(save) as OkObjectResult;
            GeneralDto.Response res = result.Value as GeneralDto.Response;
            Assert.True(res.Data == null);
            Assert.True(res.Message == null);
            Assert.False(res.Error);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        //insert
        [Theory, ClassData(typeof(CountryControllerTheory.SaveCountryOkParamsWithoutId))]
        public void SaveCountry_WithoutId_ThanOKRequest(CountryDto.Save save)
        {
            OkObjectResult result = CountryController.Save(save) as OkObjectResult;
            GeneralDto.Response res = result.Value as GeneralDto.Response;
            Assert.True(res.Data == null);
            Assert.True(res.Message != "Invalid country!");
            Assert.False(res.Error);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

    }
}

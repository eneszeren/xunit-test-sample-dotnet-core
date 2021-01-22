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
    public class CityControllerTest : IClassFixture<ServiceFixture>
    {
        public CityController CityController { get; set; }

        public CityControllerTest(ServiceFixture fixture)
        {
            CityController = new CityController(fixture.CityService);
            CityController.Authorize();

        }

        [Fact]
        public void ListCity_WithNullData_ThanOkRequest()
        {
            OkObjectResult result = CityController.List() as OkObjectResult;
            GeneralDto.Response res = (GeneralDto.Response)result.Value;
            var data = res.Data as List<CityDto.Detail>;
            Assert.True(data.Count > 0);
            Assert.False(res.Error);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [ClassData(typeof(CityControllerTheory.DetailCityOkParams))]
        public void DetailCity_WithData_ThanOkRequest(GeneralDto.DetailRequest detailRequest)
        {

            OkObjectResult result = CityController.Detail(detailRequest) as OkObjectResult;
            var res = result.Value as GeneralDto.Response;
            Assert.True(res.Data != null);
            Assert.False(res.Error);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [ClassData(typeof(CityControllerTheory.DetailCityBadParams))]
        public void DetailCity_WithFalseData_ThanOkRequest(GeneralDto.DetailRequest detailRequest)
        {
            OkObjectResult result = CityController.Detail(detailRequest) as OkObjectResult;
            var res = result.Value as GeneralDto.Response;
            Assert.True(res.Data == null);
            Assert.True(res.Message != null);
            Assert.True(res.Error);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        //update
        [Theory]
        [ClassData(typeof(CityControllerTheory.SaveCityOkParamsWithId))]
        public void SaveCity_WithId_ThanOKRequest(CityDto.Save save)
        {
            OkObjectResult result = CityController.Save(save) as OkObjectResult;
            var res = result.Value as GeneralDto.Response;
            Assert.True(res.Data == null);
            Assert.True(res.Message == null);
            Assert.False(res.Error);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        //insert
        [Theory]
        [ClassData(typeof(CityControllerTheory.SaveCityOkParamsWithoutId))]
        public void SaveCity_WithoutId_ThanOKRequest(CityDto.Save save)
        {
            OkObjectResult result = CityController.Save(save) as OkObjectResult;
            var res = result.Value as GeneralDto.Response;
            Assert.True(res.Data == null);
            Assert.True(res.Message == null);
            Assert.False(res.Error);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

    }
}

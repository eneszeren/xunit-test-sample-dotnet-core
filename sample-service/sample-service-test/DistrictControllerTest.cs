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
    public class DistrictControllerTest : IClassFixture<ServiceFixture>
    {
        public DistrictController DistrictController { get; set; }

        public DistrictControllerTest(ServiceFixture fixture)
        {
            DistrictController = new DistrictController(fixture.DistrictService);
            DistrictController.Authorize();
        }

        [Fact]
        public void ListDistrict_WithNullData_ThanOkRequest()
        {
            OkObjectResult result = DistrictController.List() as OkObjectResult;
            GeneralDto.Response res = (GeneralDto.Response)result.Value;
            var data = res.Data as List<DistrictDto.Detail>;
            Assert.True(data.Count > 0);
            Assert.False(res.Error);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [ClassData(typeof(DistrictControllerTheory.DetailDistrictOkParams))]
        public void DetailDistrict_WithData_ThanOkRequest(GeneralDto.DetailRequest detailRequest)
        {

            OkObjectResult result = DistrictController.Detail(detailRequest) as OkObjectResult;
            var res = result.Value as GeneralDto.Response;
            Assert.True(res.Data != null);
            Assert.False(res.Error);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [ClassData(typeof(DistrictControllerTheory.DetailDistrictBadParams))]
        public void DetailDistrict_WithFalseData_ThanOkRequest(GeneralDto.DetailRequest detailRequest)
        {
            OkObjectResult result = DistrictController.Detail(detailRequest) as OkObjectResult;
            var res = result.Value as GeneralDto.Response;
            Assert.True(res.Data == null);
            Assert.True(res.Message != null);
            Assert.True(res.Error == true);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        //update
        [Theory]
        [ClassData(typeof(DistrictControllerTheory.SaveDistrictOkParams))]
        public void SaveDistrict_WithId_ThanOKRequest(DistrictDto.Save save)
        {
            OkObjectResult result = DistrictController.Save(save) as OkObjectResult;
            var res = result.Value as GeneralDto.Response;
            Assert.True(res.Data == null);
            Assert.True(res.Message == null);
            Assert.False(res.Error);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        //insert
        [Theory]
        [ClassData(typeof(DistrictControllerTheory.SaveDistrictOkParamsWithoutId))]
        public void SaveDistrict_WithoutId_ThanOKRequest(DistrictDto.Save save)
        {
            OkObjectResult result = DistrictController.Save(save) as OkObjectResult;
            var res = result.Value as GeneralDto.Response;
            Assert.True(res.Data == null);
            Assert.True(res.Message != "Invalid district!");
            Assert.False(res.Error);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

    }
}

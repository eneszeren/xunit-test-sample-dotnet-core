using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sample_service.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sample_service.Dtos;
using sample_service.Services;

namespace sample_service.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        readonly IDistrictService _serviceDistrict;

        public DistrictController(IDistrictService districtService)
        {
            _serviceDistrict = districtService;
        }

        [HttpGet("List")]
        public IActionResult List()
        {
            try
            {
                return Ok(_serviceDistrict.List());
            }
            catch (Exception)
            {
                return BadRequest();                
            }
        }

        [HttpPost("Detail")]
        public IActionResult Detail(GeneralDto.DetailRequest detailRequest)
        {
            try
            {                
                return Ok(_serviceDistrict.Detail(detailRequest));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Save")]
        public IActionResult Save(DistrictDto.Save save)
        {
            try
            {
                return Ok(_serviceDistrict.Save(save, User.Identity.Name.ToInt()));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

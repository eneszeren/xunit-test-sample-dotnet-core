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
    public class CountryController : ControllerBase
    {
        readonly ICountryService _serviceCountry;

        public CountryController(ICountryService countryService)
        {
            _serviceCountry = countryService;
        }

        [HttpGet("List")]
        public IActionResult List()
        {
            try
            {
                return Ok(_serviceCountry.List());
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
                return Ok(_serviceCountry.Detail(detailRequest));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Save")]
        public IActionResult Save(CountryDto.Save save)
        {
            try
            {
                return Ok(_serviceCountry.Save(save, User.Identity.Name.ToInt()));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

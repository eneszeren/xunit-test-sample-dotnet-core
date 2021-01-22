using sample_service.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sample_service.Entities.Sample;

namespace sample_service.Services
{
    public interface ICityService
    {
        GeneralDto.Response Detail(GeneralDto.DetailRequest id);
        GeneralDto.Response Save(CityDto.Save save ,int userId);
        GeneralDto.Response List();
        List<GeneralDto.Select> SelectList();

    }
    public class CityService : ICityService
    {
        readonly SampleDbContext _context;
        readonly ICountryService _serviceCountry;
        public CityService(SampleDbContext context, ICountryService countryService)
        {
            _context = context;
            _serviceCountry = countryService;
        }

        public GeneralDto.Response Detail(GeneralDto.DetailRequest detailRequest)
        {

            try
            {
                CityDto.Detail detail = _context.City.Where(w => w.Id == detailRequest.Id)
                    .Include(i => i.Country)
                    .Select(s => new CityDto.Detail()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        CountryId= s.CountryId,
                        CountryName=s.Country.Name,
                    })
                    .FirstOrDefault();

                if (detail == null)
                {
                    return new GeneralDto.Response() { Error = true, Message = "Invalid city!" };
                }

                return new GeneralDto.Response() { Data = detail };
            }
            catch (Exception)
            {
                return new GeneralDto.Response() { Error = true, Message = "Invalid city!" };
            }
        }

        public GeneralDto.Response Save(CityDto.Save save, int userId)
        {

            try
            {
                var a = _context.City.ToList();

                if (save.Id == 0)
                {
                    City city = new City()
                    {
                        Name = save.Name,
                        CountryId = save.CountryId
                    };

                    _context.City.Add(city);
                }
                else
                {
                    City city = _context.City.Where(w => w.Id == save.Id).FirstOrDefault();
                    if (city == null)
                    {
                        return new GeneralDto.Response() { Error = true, Message = "Invalid city!" };
                    }
                    city.Name = save.Name;
                    city.CountryId = save.CountryId;
                }

                _context.SaveChanges();

                var b = _context.City.ToList();


                return new GeneralDto.Response();
            }
            catch (Exception)
            {
                return new GeneralDto.Response(true);
            }
        }

        public GeneralDto.Response List()
        {

            try
            {
                List<CityDto.Detail> detailList = _context.City
                    .Include(i => i.Country)
                    .Select(s => new CityDto.Detail()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        CountryId = s.CountryId,
                        CountryName = s.Country.Name
                    })
                    .ToList();                

                return new GeneralDto.Response() { Data = detailList, Common = new { Country = _serviceCountry.SelectList() } };
            }
            catch (Exception)
            {
                return new GeneralDto.Response(true);
            }
        }
        public List<GeneralDto.Select> SelectList()
        {
            try
            {
                return _context.City.Select(s => new GeneralDto.Select()
                {
                    Value = s.Id,
                    Label = s.Name,
                }).ToList();
            }
            catch (Exception)
            {
                return new List<GeneralDto.Select>();
            }
        }
    }
}

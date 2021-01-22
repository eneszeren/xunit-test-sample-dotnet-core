using sample_service.Dtos;
using sample_service.Entities.Sample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_service.Services
{
    public interface ICountryService
    {
        GeneralDto.Response Detail(GeneralDto.DetailRequest id);
        GeneralDto.Response Save(CountryDto.Save save, int userId);
        GeneralDto.Response List();
        List<GeneralDto.Select> SelectList();

    }
    public class CountryService : ICountryService
    {
        readonly SampleDbContext _context;
        public CountryService(SampleDbContext context)
        {
            _context = context;
        }

        public GeneralDto.Response Detail(GeneralDto.DetailRequest detailRequest)
        {

            try
            {
                CountryDto.Detail detail = _context.Country.Where(w => w.Id == detailRequest.Id)
                    .Select(s => new CountryDto.Detail()
                    {
                        Id = s.Id,
                        Name = s.Name,
                    })
                    .FirstOrDefault();

                if (detail == null)
                {
                    return new GeneralDto.Response() { Error = true, Message = "Invalid country!" };
                }

                return new GeneralDto.Response() { Data = detail };
            }
            catch (Exception)
            {
                return new GeneralDto.Response() { Error = true, Message = "Invalid country!" };
            }
        }

        public GeneralDto.Response Save(CountryDto.Save save, int userId)
        {

            try
            {
                var a = _context.Country.ToList();


                if (save.Id == 0)
                {
                    Country country = new Country()
                    {
                        Name = save.Name,
                    };

                    _context.Country.Add(country);
                }
                else
                {
                    Country country = _context.Country.Where(w => w.Id == save.Id).FirstOrDefault();
                    if (country == null)
                    {
                        return new GeneralDto.Response() { Error = true, Message = "Invalid country!" };
                    }
                    country.Name = save.Name;
                }

                _context.SaveChanges();

                var b = _context.Country.ToList();


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
                List<CountryDto.Detail> detailList = _context.Country
                    .Select(s => new CountryDto.Detail()
                    {
                        Id = s.Id,
                        Name = s.Name,
                    })
                    .ToList();

                return new GeneralDto.Response() { Data = detailList };
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
                return _context.Country
                    .Select(s => new GeneralDto.Select()
                    {
                        Value = s.Id,
                        Label = s.Name,
                    })
                    .ToList();
            }
            catch (Exception)
            {
                return new List<GeneralDto.Select>();
            }
        }
    }
}

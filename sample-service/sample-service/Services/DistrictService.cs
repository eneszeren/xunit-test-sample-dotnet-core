using sample_service.Dtos;
using sample_service.Entities.Sample;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_service.Services
{
    public interface IDistrictService
    {
        GeneralDto.Response Detail(GeneralDto.DetailRequest id);
        GeneralDto.Response Save(DistrictDto.Save save, int userId);
        GeneralDto.Response List();
        List<GeneralDto.Select> SelectList();

    }
    public class DistrictService : IDistrictService
    {
        readonly SampleDbContext _context;
        readonly ICityService _serviceCity;
        public DistrictService(SampleDbContext context, ICityService cityService)
        {
            _context = context;
            _serviceCity = cityService;
        }

        public GeneralDto.Response Detail(GeneralDto.DetailRequest detailRequest)
        {

            try
            {
                DistrictDto.Detail detail = _context.District.Where(w => w.Id == detailRequest.Id)
                    .Include(i => i.City)
                    .Select(s => new DistrictDto.Detail()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        CityId = s.CityId,
                        CityName = s.City.Name
                    })
                    .FirstOrDefault();

                if (detail == null)
                {
                    return new GeneralDto.Response() { Error = true, Message = "Invalid district!" };
                }

                return new GeneralDto.Response() { Data = detail };
            }
            catch (Exception exp)
            {
                return new GeneralDto.Response() { Error = true, Message = exp.ToString() };
            }
        }

        public GeneralDto.Response Save(DistrictDto.Save save, int userId)
        {

            try
            {
                if (save.Id == 0)
                {
                    District district = new District()
                    {
                        Name = save.Name,
                    };

                    _context.District.Add(district);
                }
                else
                {
                    District district = _context.District.Where(w => w.Id == save.Id).FirstOrDefault();
                    if (district == null)
                    {
                        return new GeneralDto.Response() { Error = true, Message = "Invalid district!" };
                    }
                    district.Name = save.Name;
                }

                _context.SaveChanges();

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
                List<DistrictDto.Detail> detailList = _context.District
                    .Include(i => i.City)
                    .Select(s => new DistrictDto.Detail()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        CityId = s.CityId,
                        CityName = s.City.Name
                    })
                    .ToList();

                return new GeneralDto.Response() { Data = detailList, Common = new { City = _serviceCity.SelectList() } };
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
                return _context.District
                    .Include(i => i.City)
                    .Select(s => new GeneralDto.Select()
                    {
                        Value = s.Id,
                        Label = s.Name,
                        Common = s.City.Id,
                    }).ToList();
            }
            catch (Exception)
            {
                return new List<GeneralDto.Select>();
            }
        }
    }
}

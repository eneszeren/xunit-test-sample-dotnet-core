using sample_service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace sample_service_test.Theory
{
    public class CityControllerTheory
    {
        public class DetailCityOkParams : TheoryData<GeneralDto.DetailRequest>
        {
            public DetailCityOkParams()
            {
                Add(new GeneralDto.DetailRequest()
                {
                    //Turkey
                    Id = 1,
                });
            }
        }

        public class DetailCityBadParams : TheoryData<GeneralDto.DetailRequest>
        {
            public DetailCityBadParams()
            {
                Add(new GeneralDto.DetailRequest()
                {
                    Id = 0,
                });
            }
        }

        //update
        public class SaveCityOkParamsWithId : TheoryData<CityDto.Save>
        {
            public SaveCityOkParamsWithId()
            {
                Add(new CityDto.Save()
                {
                    Description = "eq9wr6",
                    Id = 1,
                    Name = "k1q4ux",
                    CountryId = 1,
                });
            }
        }

        //insert
        public class SaveCityOkParamsWithoutId : TheoryData<CityDto.Save>
        {
            public SaveCityOkParamsWithoutId()
            {
                Add(new CityDto.Save()
                {
                    Id = 0,
                    Description = "di25kq",
                    Name = "laim3d",
                    CountryId = 1,
                });
            }
        }

    }
}

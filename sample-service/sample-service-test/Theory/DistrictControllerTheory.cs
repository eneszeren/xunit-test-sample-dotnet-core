using sample_service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace sample_service_test.Theory
{
    public class DistrictControllerTheory
    {
        public class DetailDistrictOkParams : TheoryData<GeneralDto.DetailRequest>
        {
            public DetailDistrictOkParams()
            {
                Add(new GeneralDto.DetailRequest()
                {
                    //Turkey
                    Id = 1,
                });
            }
        }

        public class DetailDistrictBadParams : TheoryData<GeneralDto.DetailRequest>
        {
            public DetailDistrictBadParams()
            {
                Add(new GeneralDto.DetailRequest()
                {
                    Id = 0,
                });
            }
        }

        //update
        public class SaveDistrictOkParams : TheoryData<DistrictDto.Save>
        {
            public SaveDistrictOkParams()
            {
                Add(new DistrictDto.Save()
                {
                    Description = "eq9wr6",
                    Id = 1,
                    Name = "k1q4ux",
                    CityId = 1,
                });
            }
        }

        //insert
        public class SaveDistrictOkParamsWithoutId : TheoryData<DistrictDto.Save>
        {
            public SaveDistrictOkParamsWithoutId()
            {
                Add(new DistrictDto.Save()
                {
                    Id = 0,
                    Description = "cl90ka",
                    Name = "5oer9x",
                    CityId = 1,
                });
            }
        }

    }
}

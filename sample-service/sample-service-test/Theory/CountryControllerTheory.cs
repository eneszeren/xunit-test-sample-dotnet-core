using sample_service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace sample_service_test.Theory
{
    public class CountryControllerTheory
    {
        public class DetailCountryOkParams : TheoryData<GeneralDto.DetailRequest>
        {
            public DetailCountryOkParams()
            {
                Add(new GeneralDto.DetailRequest()
                {
                    //Turkey
                    Id = 1,
                });
            }

        }

        public class DetailCountryBadParams : TheoryData<GeneralDto.DetailRequest>
        {
            public DetailCountryBadParams()
            {
                Add(new GeneralDto.DetailRequest()
                {
                    Id = 0,
                });
            }
        }

        //update
        public class SaveCountryOkParams : TheoryData<CountryDto.Save>
        {
            public SaveCountryOkParams()
            {
                Add(new CountryDto.Save()
                {
                    Description = "eq9wr6",
                    Id = 1,
                    Name = "k1q4ux",
                });
            }
        }

        //insert
        public class SaveCountryOkParamsWithoutId : TheoryData<CountryDto.Save>
        {
            public SaveCountryOkParamsWithoutId()
            {
                Add(new CountryDto.Save()
                {
                    Id = 0,
                    Description = "2e3g3q",
                    Name = "895net",
                });
            }
        }



    }
}

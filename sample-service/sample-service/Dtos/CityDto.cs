using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_service.Dtos
{
    public class CityDto
    {
        public class Save
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int CountryId { get; set; }
        }

        public class Detail : Save
        {
            public string CountryName { get; set; }
        }
    }
}

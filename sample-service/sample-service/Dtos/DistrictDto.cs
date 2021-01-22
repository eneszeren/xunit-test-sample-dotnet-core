﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_service.Dtos
{
    public class DistrictDto
    {
        public class Save
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int CityId { get; set; }

        }

        public class Detail : Save
        {
            public string CityName { get; set; }
        }
    }
}

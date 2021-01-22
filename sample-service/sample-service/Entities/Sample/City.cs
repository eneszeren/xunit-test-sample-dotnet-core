using System;
using System.Collections.Generic;

namespace sample_service.Entities.Sample
{
    public partial class City
    {
        public City()
        {
            District = new HashSet<District>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<District> District { get; set; }
    }
}

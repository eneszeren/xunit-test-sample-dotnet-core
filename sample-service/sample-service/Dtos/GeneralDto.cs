using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_service.Dtos
{
    public class GeneralDto
    {
        public class Response
        {
            public bool Error { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
            public object Common { get; set; }
        }

        public class Detail
        {
            public int Id { get; set; }
        }
        public class Delete : Detail
        {
            public int UserId { get; set; } = 0;
        }
        public class Select
        {
            public string label { get; set; }
            public object value { get; set; }
            public object common { get; set; }
        }

        public class UserSelect:Select
        {
            public string email { get; set; }
        }
    }
}

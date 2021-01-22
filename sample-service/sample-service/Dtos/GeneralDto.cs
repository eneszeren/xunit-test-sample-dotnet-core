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
              public Response(bool isError = false)
            {
                if (isError == true)
                {
                    Error = true;
                    Message = "An error was occured!";
                }
            }

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

        public class DetailRequest
        {
            public int Id { get; set; }
        }

        public class Select
        {
            public string Label { get; set; }
            public object Value { get; set; }
            public object Common { get; set; }
        }

        public class UserSelect : Select
        {
            public string Email { get; set; }
        }
    }
}

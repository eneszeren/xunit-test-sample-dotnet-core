using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_service.Dtos
{
    public class UserDto
    {
        public class LoginRequest
        {
            public string email { get; set; }
            public string password { get; set; }
        }

        public class Authorized
        {
            public int Id { get; set; }
            public string Token { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
        }

        public class TransferUser
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public bool Status { get; set; }
        }

        public class RecoverPassword
        {
            public string Email { get; set; }
            public int Admin { get; set; } = 0;
        }

        public class ResetPassword
        {
            public string Email { get; set; }
            public string Token { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }

        public class Save
        {
            public int Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Email { get; set; }
            public bool? Status { get; set; }
            public int UserId { get; internal set; }
        }

        public class Detail : Save
        {
            public DateTime CreateDate { get; set; }
        }

        public class GetEmail
        {
            public string Email { get; set; }
        }

    }


}

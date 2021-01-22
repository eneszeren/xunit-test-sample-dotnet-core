using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sample_service.Dtos;
using sample_service.Helpers;
using sample_service.Entities.Sample;

namespace sample_service.Services
{
    public interface IUserService
    {

        GeneralDto.Response Login(UserDto.LoginRequest login, HttpRequest request);
        GeneralDto.Response ResetPassword(UserDto.ResetPassword model, HttpRequest request);
        GeneralDto.Response Detail(GeneralDto.Detail id);
        GeneralDto.Response Save(UserDto.Save save, HttpRequest request);
        GeneralDto.Response Delete(GeneralDto.Delete id);
        GeneralDto.Response List();
        List<GeneralDto.UserSelect> SelectList();
        GeneralDto.Response GetUserGetUserEmail(int userId);
    }

    public class UserService : IUserService
    {
        SampleDbContext _context;
        AppSettings _appSettings;

        public UserService(SampleDbContext context,IOptions<AppSettings> options)
        {
            _context = context;
            _appSettings = options.Value;
        }

        public GeneralDto.Response Login(UserDto.LoginRequest login, HttpRequest request)
        {
            try
            {
                User user = _context.User
                    .Where(w => w.Email == login.email && w.Password == login.password && w.Status.GetValueOrDefault(true))
                    .FirstOrDefault();
                if (user == null)
                    return null;

                UserDto.Authorized result = new UserDto.Authorized
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = string.Concat(user.Firstname, " ", user.Lastname)
                };

                return new GeneralDto.Response() { Data = result };
            }
            catch (Exception exp)
            {

                return new GeneralDto.Response() { Error = true, Message = "An error was occured!" };
            }
        }

        public GeneralDto.Response ResetPassword(UserDto.ResetPassword model, HttpRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Token) || string.IsNullOrEmpty(model.Password) || model.Password != model.ConfirmPassword)
                    return new GeneralDto.Response() { Error = true, Message = "All areas are required!" };
                if (model.Password.Length < 6)
                    return new GeneralDto.Response() { Error = true, Message = "Password length must be at least 6 characters" };

                User user = _context.User.Where(w => w.Email == model.Email).FirstOrDefault();
                if (user == null)
                    return new GeneralDto.Response() { Error = true, Message = "Invalid email address for your token" };

                string token = $"{user.Email}{user.Password}{user.Firstname}{user.Lastname}";
                if (token != model.Token)
                    return new GeneralDto.Response() { Error = true, Message = "Invalid token!" };

                user.Password = model.Password;
                _context.SaveChanges();

                return new GeneralDto.Response() { Error = false, Message = "Your password has been successfully updated." };
            }
            catch (Exception exp)
            {

                return new GeneralDto.Response() { Error = true, Message = "An error was occured!" };
            }
        }

        public GeneralDto.Response Delete(GeneralDto.Delete id)
        {
            try
            {
                User user = _context.User.Where(w => w.Id == id.Id).FirstOrDefault();
                if (user == null)
                {
                    return new GeneralDto.Response() { Error = true, Message = "Invalid user!" };
                }

                user.Status = false;

                _context.SaveChanges();

                return new GeneralDto.Response();
            }
            catch (Exception exp)
            {

                return new GeneralDto.Response() { Error = true, Message = "An error was occured!" + exp.ToString() };
            }
        }

        public GeneralDto.Response Detail(GeneralDto.Detail id)
        {
            try
            {
                UserDto.Detail detail = _context.User.Where(w => w.Id == id.Id)
                    .Select(s => new UserDto.Detail()
                    {
                        Status = s.Status,
                        CreateDate = s.CreateDate,
                        Email = s.Email,
                        Firstname = s.Firstname,
                        Id = s.Id,
                        Lastname = s.Lastname,
                    })
                    .FirstOrDefault();

                if (detail == null)
                {
                    return new GeneralDto.Response() { Error = true, Message = "Invalid user!" };
                }

                return new GeneralDto.Response() { Data = detail };
            }
            catch (Exception exp)
            {

                return new GeneralDto.Response() { Error = true, Message = "An error was occured!" };
            }
        }

        public GeneralDto.Response Save(UserDto.Save save, HttpRequest request)
        {

            try
            {
                if (save.Id == 0)
                {
                    User user = new User()
                    {
                        Status = save.Status,
                        Password = "TempPassword",
                        CreateDate = DateTime.Now,
                        Email = save.Email,
                        Firstname = save.Firstname,
                        Lastname = save.Lastname,
                    };

                    _context.User.Add(user);
                    _context.SaveChanges();

                }
                else
                {
                    User user = _context.User.Where(w => w.Id == save.Id).FirstOrDefault();
                    if (user == null)
                    {
                        return new GeneralDto.Response() { Error = true, Message = "Invalid user!" };
                    }
                    user.Status = save.Status;
                    user.Email = save.Email;
                    user.Firstname = save.Firstname;
                    user.Lastname = save.Lastname;
                }

                _context.SaveChanges();

                return new GeneralDto.Response();
            }
            catch (Exception exp)
            {

                return new GeneralDto.Response() { Error = true, Message = "An error was occured!" };
            }
        }

        public GeneralDto.Response List()
        {
            try
            {
                List<UserDto.Detail> detailList = _context.User
                    .Select(s => new UserDto.Detail()
                    {
                        Status = s.Status,
                        CreateDate = s.CreateDate,
                        Email = s.Email,
                        Firstname = s.Firstname,
                        Id = s.Id,
                        Lastname = s.Lastname,
                    })
                    .ToList();

                return new GeneralDto.Response() { Data = detailList };
            }
            catch (Exception exp)
            {

                return new GeneralDto.Response() { Error = true, Message = "An error was occured!" };
            }
        }

        public List<GeneralDto.UserSelect> SelectList()
        {
            try
            {
                return _context.User
                    .Select(s => new GeneralDto.UserSelect()
                    {
                        value = s.Id,
                        label = string.Concat(s.Firstname, " ", s.Lastname),
                        email = s.Email
                    }).ToList();
            }
            catch (Exception exp)
            {

                return new List<GeneralDto.UserSelect>();
            }
        }

        public GeneralDto.Response GetUserGetUserEmail(int userId)
        {
            try
            {

                UserDto.GetEmail detail = _context.User.Where(w => w.Id == userId)
                    .Select(s => new UserDto.GetEmail()
                    {
                        Email = s.Email
                    }).FirstOrDefault();

                if (detail == null)
                {
                    return new GeneralDto.Response() { Error = true, Message = "Invalid user!" };
                }

                return new GeneralDto.Response() { Data = detail };
            }
            catch (Exception exp)
            {

                return new GeneralDto.Response() { Error = true, Message = "An error was occured!" };
            }
        }

    }
}

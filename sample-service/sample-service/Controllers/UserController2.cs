﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace sample_service.Controllers
{
    public class UserController2 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
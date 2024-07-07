using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Error;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController:BaseApiController
    {
        DataContext _datacontext;
        public BuggyController(DataContext datacontext)
        {
            _datacontext = datacontext;
        }

        [HttpGet("auth")]
        
        public ActionResult<string> getSecret()
        {
            return Unauthorized("secrectresult");
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> notfound()
        {
            AppUser user=_datacontext.Users.Find(-1);
            return user==null ? NotFound() : user;
        }
        

        [HttpGet("server-error")]
        public ActionResult<string> ServerError()
        {
            AppUser user=_datacontext.Users.Find(-1);
            string userdetails=user.ToString();
            // var errordevelopment=new Errorpropogate("Hi","Internal Server Error",500);
            // var errorproduction=new Errorpropogate("Hi","Internal Server Error",500);
            // string userdetails=JsonSerializer.Serialize(errordevelopment);
            return userdetails;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> badrequest()
        {
            return BadRequest("Not good");
        }


    }
}
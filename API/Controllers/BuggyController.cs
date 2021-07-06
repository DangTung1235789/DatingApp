using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //7. Error handling
    //it simply to return errors so that we can see what we get back from varius different responses  
    public class BuggyController : BaseApiController
    {
        //create constructor and inject dataContext 
        private readonly DataContext _context;
        public BuggyController(DataContext context) 
        { 
            _context = context;
        }
        //we going to generate several different method that are all return of response not successful
        //go to startup.cs
        [Authorize]
        [HttpGet("auth")]
        //the purpose of this is to test 401 ( Unauthorised responses )
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }
        [HttpGet("not found")]
        //the purpose of this is to test 401 ( Unauthorised responses )
        public ActionResult<AppUser> GetNotFound()
        {
            //we'll look for smt that we know for sure is not going to exist 
            var thing = _context.Users.Find(-1);
            if(thing == null ){
                return NotFound();
            }

            //if we do somehow manage to find this, we'll return to this 
            return Ok(thing);
        }
        [HttpGet("server-error")]
        //the purpose of this is to test 401 ( Unauthorised responses )
        public ActionResult<string> GetServerError()
        {
            //we'll look for smt that we know for sure is not going to exist 
            var thing = _context.Users.Find(-1);
            //we want to do is genarate an exception from this particular method 
            // when you don't receive the parameter that you expecting and you try and executed a method on smt that's null 
            //then u'll always get a no reference exception 
            var thingToReturn = thing.ToString();
            return thingToReturn;
           
        }
        [HttpGet("bad-request")]
        //the purpose of this is to test 401 ( Unauthorised responses )
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest();
        }
    }
}
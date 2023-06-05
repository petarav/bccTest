using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;

namespace bccTest.Controllers
{
    public static class Globals
    {
        public static List<string> Items = 
            new List<string>
            {
                "1. ToDo stavka",
                "2. ToDo stavka"
            };
        public static string[,] Users =
            new string[,]
            {
                { "admin", "admin", "admin"},
                { "user", "user", "user"},
                {"marko", "matan", "admin" }
            };
        public static string role = "Anonymous";
    }

    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        [HttpGet]
        public List<string> Get()
        {
            if (Globals.role != "Anonymous")
                return Globals.Items;
            else
                return null;
        }

        [HttpGet("AddtoDostr")]
        public IActionResult Get(string toDostr)
        {
            if (toDostr == null || Globals.role!="admin") { return BadRequest(); }
            else
            {
                Globals.Items.Add(toDostr);
                return Ok();
            }
        }

        [HttpGet("{username}/{password}")]
        public string Get(string username,string password)
        {
            Globals.role = "Anonymous";

            for(int i =0;i<=2; i++)
                if (Globals.Users[i, 0] == username && Globals.Users[i, 1] == password)
                    Globals.role = Globals.Users[i, 2];
            return Globals.role;
        }
    }
}

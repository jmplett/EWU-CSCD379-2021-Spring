using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // /api/events
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return DeleteMe.Users;
        }

        // /api/events/<index>
        [HttpGet("{index}")]
        public string Get(int index)
        {
            return DeleteMe.Users[index];
        }

        //DELETE /api/events/<index>
        [HttpDelete("{index}")]
        public void Delete(int index)
        {
            DeleteMe.Users.RemoveAt(index);
        }

        // POST /api/events
        [HttpPost]
        public void Post([FromBody] string eventName)
        {
            DeleteMe.Users.Add(eventName);
        }

        // PUT /api/events
        [HttpPut("{index}")]
        public void Put(int index, [FromBody]string userName)
        {
            DeleteMe.Users[index] = userName;
        }
    }
}
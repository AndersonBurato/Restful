using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restful.Repository;
using Restful.Repository.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Restful.Controllers
{
    [Route("api/Person/{Personid}")]
    [ApiController]
    public class PersonAddressController : ControllerBase
    {
        [HttpGet("Address")]
        public IActionResult Get([FromRoute] int personid, [FromServices] DataBase dataBase)
        {
            var result = dataBase.Addresses.Include(x => x.Person).Where(x => x.PersonId == personid);

            if (!result.Any())
                return StatusCode(204, string.Empty);

            return Ok(result);
        }

        [HttpPost("Address")]
        public IActionResult Post([FromRoute] int personId, [FromBody] Address address, [FromServices] DataBase dataBase)
        {
            if (!dataBase.People.Where(x => x.Id == personId).Any())
                return StatusCode(404, $"Person id {personId} does not exist");

            address.PersonId = personId;

            dataBase.Addresses.Add(address);

            dataBase.SaveChanges();

            return Ok();
        }
    }
}

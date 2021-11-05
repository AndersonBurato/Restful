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
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromServices] DataBase dataBase)
        {
            var result = dataBase.People.Include(x => x.Addresses).Select(x => x);

            if (result.Any())
                return Ok(result);
            else
                return StatusCode(204, string.Empty);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person, [FromServices] DataBase dataBase)
        {
            dataBase.Add(person);

            dataBase.SaveChanges();

            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch([FromRoute] int id, [FromBody] Person person, [FromServices] DataBase dataBase)
        {
            if (string.IsNullOrWhiteSpace(person.FirstName))
                return StatusCode(400, $"Missing Parameter {nameof(person.FirstName)}");

            if (string.IsNullOrWhiteSpace(person.LastName))
                return StatusCode(400, $"Missing Parameter {nameof(person.LastName)}");

            var personDb = dataBase.People.Where(x => x.Id == id).FirstOrDefault();

            if (personDb == null)
                return StatusCode(404, $"Person id {id} does not exist");

            personDb.FirstName = person.FirstName;
            personDb.LastName = person.LastName;

            dataBase.Update(personDb);

            dataBase.SaveChanges();

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] DataBase dataBase)
        {
            var peopleToRemove = dataBase.People.Where(x => x.Id == id);

            dataBase.People.RemoveRange(peopleToRemove);

            dataBase.SaveChanges();

            return Ok();
        }
    }
}

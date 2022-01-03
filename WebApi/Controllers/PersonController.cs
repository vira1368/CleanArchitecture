using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using WebFramework.Api;
using WebFramework.Filters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ApiResultFilter]
        public async Task<ActionResult<List<Person>>> Get()
        {
            var people = _context.People.ToList();
            return Ok(people);
        }

        [HttpGet("{id:int}")]
        [ApiResultFilter]
        public ActionResult<Person> Get(int id)
        {
            var person = _context.People.SingleOrDefault(p => p.Id == id);
            return Ok(person);
        }

        [HttpPost]
        [ApiResultFilter]
        public async Task<ActionResult<Person>> Create(Person person)
        {
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
            return Ok(person);
        }

        [HttpPut]
        [ApiResultFilter]
        public async Task<ActionResult> Update(Person person)
        {
            var personInDb = await _context.People.FindAsync(person.Id);
            if (personInDb == null)
                return BadRequest();
            personInDb.FirstName = person.FirstName;
            personInDb.LastName = person.LastName;
            personInDb.Age = person.Age;
            personInDb.Gender = person.Gender;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ApiResultFilter]
        public async Task<ActionResult> Delete(int id)
        {
            var personInDb = await _context.People.FindAsync(id);
            if (personInDb == null)
                return BadRequest();
            _context.People.Remove(personInDb);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

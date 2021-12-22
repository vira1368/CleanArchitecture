using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;

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
        public async Task<List<Person>> Get()
        {
            return _context.People.ToList();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Person> Get(int id)
        {
            return _context.People.SingleOrDefault(p => p.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
            return Ok(person);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Person person)
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
        public async Task<IActionResult> Delete(int id)
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

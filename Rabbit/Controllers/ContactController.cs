using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rabbit.Db.Models;
using Rabbit.Repositories.Interfaces;
using Rabbit.Services.Interfaces;

namespace Rabbit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IRedisCacheService _redisCacheService;
        public ContactController(IContactRepository contactRepository, IRedisCacheService redisCacheService)
        {
            _contactRepository = contactRepository;
            _redisCacheService = redisCacheService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            var contacts = _redisCacheService.GetData<IEnumerable<Contact>>("contacts");

            if(contacts is not null)
            {
                return Ok(contacts);
            }

            contacts =  await _contactRepository.GetAllAsync();
            _redisCacheService.SetData("contacts", contacts);

            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(long id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact == null) return NotFound();
            return contact;
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact([FromBody] Contact contact)
        {
            await _contactRepository.AddAsync(contact);
            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(long id, [FromBody] Contact contact)
        {
            if (id != contact.Id) return BadRequest();
            await _contactRepository.UpdateAsync(contact);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(long id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact == null) return NotFound();
            await _contactRepository.DeleteAsync(contact);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Moonlay.Contacts.Application;
using Moonlay.Contacts.Domain;
using Moonlay.Contacts.Models;
using Moonlay.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moonlay.Baas.Contacts.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactService contactService, IContactRepository contactRepository)
        {
            _contactService = contactService;
            _contactRepository = contactRepository;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetAsync(int page = 0, int page_size = 25)
        {
            var query = await _contactRepository.GetAllAsync();
            var listOfContacts = query.Skip(page * page_size).Take(page_size).ToList();


            string requestId = this.Request.Query["request_id"];

            var result = new GenericResponse<IEnumerable<ContactDto>>(true, listOfContacts.Select(o => new ContactDto { Id = o.Id, Names = o.Names }), requestId: requestId);

            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
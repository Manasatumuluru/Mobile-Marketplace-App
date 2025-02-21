using EduLearnAPI;
using EduLearnAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EduLearnAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IMongoCollection<Contact> _contactCollection;

        public ContactController(EduLearnDbServices dbServices)
        {
            _contactCollection = dbServices.GetContactCollection();
        }

        [HttpPost]
        public IActionResult userContactRequest([FromBody] Contact contact)
        {
            _contactCollection.InsertOne(contact);
            return Ok("Request sent successfully.");
        }

        [HttpGet]
        public IActionResult GetContactsCount()
        {
            var count = _contactCollection.CountDocuments(FilterDefinition<Contact>.Empty);
            return Ok(count);
        }
    }
}
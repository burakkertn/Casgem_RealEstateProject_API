using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Casgem_RealEstateProject_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstateController : ControllerBase
    {
        private readonly IEstateService _estateService;

        public EstateController(IEstateService estateService)
        {
            _estateService = estateService;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Estate>> Get()
        {
            return _estateService.Get();
        }

        [HttpGet("GetById")]
        public ActionResult<Estate> Get(string id)
        {
            var essate = _estateService.Get(id);


            return essate;
        }

        [HttpPost("Insert")]
        public ActionResult<Estate> Post([FromBody] Estate estate)
        {
            estate.Id = ObjectId.GenerateNewId().ToString();
            _estateService.Create(estate);

            return CreatedAtAction(nameof(Get), new { id = estate.Id }, estate);
        }


        [HttpPut("Update")]
        public ActionResult Put(string id, [FromBody] Estate estate)
        {
            var existingEssate = _estateService.Get(id);


            _estateService.Update(id, estate);
            return NoContent();
        }
        [HttpDelete("Delete")]
        public ActionResult Delete(string id)
        {
            var essate = _estateService.Get(id);

            _estateService.Remove(essate.Id);
            return Ok();
        }
        [HttpGet("filter")]
        public ActionResult<List<Estate>> GetByFilter([FromQuery] string? city = null, [FromQuery] string? type = null,
            [FromQuery] int? room = null, [FromQuery] string? title = null, [FromQuery] int? price = null, [FromQuery] string? buildYear = null)
        {
            var estate = _estateService.GetByFilter(city, type, room, title, price, buildYear);
            return Ok(estate);
        }
    }
}


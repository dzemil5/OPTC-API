using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using OPTC_API.Models;
using OPTC_API.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace OPTC_API.Controllers
{
    [Route("api/OPTCAPI")]
    [ApiController]
    public class OPTCAPIController : ControllerBase
    {
        private readonly ILogger<OPTCAPIController> _logger;
        private readonly AppDbContext _db;

        public OPTCAPIController(AppDbContext db, ILogger<OPTCAPIController> logger)
        {
            _db = db;
            _logger = logger;
        } 

        [HttpGet]
        public ActionResult<IEnumerable<Character>> GetCharacters()
        {
            _logger.LogInformation("Getting all characters");
            return Ok(_db.Characters);
        }



        [HttpGet("{id:int}", Name = "GetCharacter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public ActionResult<Character> GetCharacter(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Failed to get Character With ID:" + id);
                return BadRequest();
            }
            var character = _db.Characters.FirstOrDefault(u => u.Id == id);

            if (character == null)
            {
                return NotFound();
            }
            return Ok(character);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public ActionResult<Character> AddCharacter([FromBody] Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(character);
            }
            if (_db.Characters.FirstOrDefault(u => u.Id == character.Id) != null)
            {
                ModelState.AddModelError("", "Character already exists");
                return BadRequest(ModelState);
            }
            if (character == null)
            {
                return BadRequest(character);
            }
            _db.Characters.AddAsync(character);

            Character model = new()
            {
                Id = character.Id,
                Name = character.Name,
                Type = character.Type,
                Class1 = character.Class1,
                Class2 = character.Class2,
                Cost = character.Cost,
                Stars = character.Stars,
                Image = character.Image
            };
            //_db.Characters.Add(model);
            _db.SaveChanges();



            return CreatedAtRoute("GetCharacter", new { id = character.Id }, character);
        }
        [HttpDelete("{id:int}", Name = "DeleteCharacter")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCharacter(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var character = _db.Characters.FirstOrDefault(u => u.Id == id);
            if (character == null)
            {
                return NotFound();
            }
            _db.Characters.Remove(character);
            _db.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateCharacter")]
        public IActionResult UpdateCharacter(int id, [FromBody] Character character)
        {
            if (character == null || id != character.Id)
            {
                return BadRequest();
            }
            Character model = new()
            {
                Id = character.Id,
                Name = character.Name,
                Type = character.Type,
                Class1 = character.Class1,
                Class2 = character.Class2,
                Cost = character.Cost,
                Stars = character.Stars,
                Image = character.Image
            };
            _db.Characters.Update(model);
            _db.SaveChanges();

            return NoContent();
        }
        [HttpPatch("{id:int}", Name = "UpdatePartialCharacter")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialCharacter(int id, JsonPatchDocument<Character> patchChar)
        {
            if (patchChar == null || id == 0)
            {
                return BadRequest();
            }
            var charac = _db.Characters.AsNoTracking().FirstOrDefault(u => u.Id == id);
            Character character = new()
            {
                Id = charac.Id,
                Name = charac.Name,
                Type = charac.Type,
                Class1 = charac.Class1,
                Class2 = charac.Class2,
                Cost = charac.Cost,
                Stars = charac.Stars,
                Image = charac.Image
            };
            patchChar.ApplyTo(character, ModelState);
            Character model = new Character()
            {
                Id = character.Id,
                Name = character.Name,
                Type = character.Type,
                Class1 = character.Class1,
                Class2 = character.Class2,
                Cost = character.Cost,
                Stars = character.Stars,
                Image = character.Image
            };
            _db.Characters.Update(model);
            _db.SaveChanges();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using OPTC_API.Models;

namespace OPTC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OPTCAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Character> GetCharacters()
        {
            
            return new List<Character> {
            new Character{Id=0002, Name= "Monkey D. Luffy", Type="STR", Class1="Fighter", Class2=null, Cost=4, Stars=3},
            new Character{Id=0005, Name= "Roronoa Zoro", Type="DEX", Class1="Slasher", Class2=null, Cost=4, Stars=3},
            new Character{Id=0009, Name= "Nami", Type="INT", Class1="Striker", Class2=null, Cost=4, Stars=3},
            new Character{Id=0013, Name= "Usopp", Type="PSY", Class1="Shooter", Class2=null, Cost=4, Stars=3},
            new Character{Id=0017, Name= "Sanji", Type="QCK", Class1="Fighter", Class2=null, Cost=4, Stars=3},
            new Character{Id=0021, Name= "Tony Tony Chopper", Type="PSY", Class1="Fighter", Class2=null, Cost=4, Stars=3}

            };
        }
        
    }
}

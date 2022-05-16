using Microsoft.AspNetCore.Mvc;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class PokemonController : ControllerBase
    {
        [HttpGet( "{PokemonName}")]
        public ActionResult Get ( string PokemonName )
        {
            return Ok( );
        }
    }
}
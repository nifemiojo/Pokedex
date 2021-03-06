using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;
using static Pokedex.Helpers.PokemonHelpers;

namespace Pokedex.Controllers
{
	[ApiController]
    [Route( "[controller]" )]
    public class PokemonController : ControllerBase
    {
        [HttpGet( "{PokemonName}")]
        public async Task<ActionResult<Pokemon>> GetPokemon ( string PokemonName )
        {
            Pokemon pokemon = await GetBasicPokemon( PokemonName);

            if ( pokemon == null )
                return NotFound();
            
            return Ok( pokemon );
        }

        [HttpGet]
        [Route( "translated/{PokemonName}" )]
        public async Task<ActionResult<Pokemon>> GetTranslatedPokemon ( string PokemonName )
        {
            Pokemon pokemon = await GetBasicPokemon( PokemonName );

            if ( pokemon == null )
                return NotFound( );

            var translated_description = await TranslatePokemonDescription( pokemon );

            pokemon.Description = translated_description;

            return Ok( pokemon );
        }
    }
}
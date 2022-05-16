using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class PokemonController : ControllerBase
    {
        [HttpGet( "{PokemonName}")]
        public async Task<ActionResult<Pokemon>> Get ( string PokemonName )
        {
            var uri = $"https://pokeapi.co/api/v2/pokemon-species/{PokemonName}";

            using var client = new HttpClient( );
            var response = await client.GetStringAsync( uri );

            JsonNode responseNode = JsonNode.Parse( response )!;
            
            Pokemon jsonString = new Pokemon
            {
                Name = responseNode? [ "name" ]?.ToString( ) ?? string.Empty,
                Description = responseNode?[ "form_descriptions" ]?.AsArray().Count == 0 ? "" : responseNode? [ "form_descriptions" ]? [ 0 ]? [ "description" ]?.ToString( ) ?? string.Empty,
                Habitat = responseNode?[ "habitat" ]? [ "name" ]?.ToString( ) ?? string.Empty,
                isLegendary = ( bool ) responseNode? [ "is_legendary" ]!
            }; 

            return Ok( jsonString );
        }
    }
}
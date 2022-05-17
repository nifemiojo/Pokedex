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

        [HttpGet]
        [Route( "translated/{PokemonName}" )]
        public async Task<ActionResult<Pokemon>> GetTranslatedPokemon ( string PokemonName )
        {
            var uri = $"https://pokeapi.co/api/v2/pokemon-species/{PokemonName}";

            using var client = new HttpClient( );
            var response = await client.GetStringAsync( uri );

            JsonNode responseNode = JsonNode.Parse( response )!;

            var description = responseNode? [ "form_descriptions" ]?.AsArray( ).Count == 0 ? "" : responseNode? [ "form_descriptions" ]? [ 0 ]? [ "description" ]?.ToString( ) ?? string.Empty;
            var habitat = responseNode? [ "habitat" ]? [ "name" ]?.ToString( ) ?? string.Empty;
            var isLegendary = ( bool ) responseNode? [ "is_legendary" ]!;
            string translated_description;

            if ( habitat == "cave" | isLegendary == true )
            {
                // Yoda Translation
                var uri3 = $"https://api.funtranslations.com/translate/yoda.json?text={description}";
                var response3 = await client.GetStringAsync( uri3 );

                JsonNode responseNode3 = JsonNode.Parse( response3 )!;

                translated_description = responseNode3? [ "contents" ]? [ "translated" ]?.ToString( ) ?? string.Empty;
            }
            else
            {
                // Shakespeare Translation
                var uri2 = $"https://api.funtranslations.com/translate/shakespeare.json?text={description}";
                var response2 = await client.GetStringAsync( uri2 );

                JsonNode responseNode2 = JsonNode.Parse( response2 )!;

                translated_description = responseNode2? [ "contents" ]? [ "translated" ]?.ToString( ) ?? string.Empty;

            }

            Pokemon jsonString = new Pokemon
            {
                Name = responseNode? [ "name" ]?.ToString( ) ?? string.Empty,
                Description = translated_description,
                Habitat = habitat,
                isLegendary = isLegendary
            };

            return Ok( jsonString );
        }
    }
}
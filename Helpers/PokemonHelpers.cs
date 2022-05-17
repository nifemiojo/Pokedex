using Pokedex.Models;
using System.Text.Json.Nodes;

namespace Pokedex.Helpers
{
	public static class PokemonHelpers
	{
        public static async Task<Pokemon> GetBasicPokemon ( string PokemonName )
        {
            var uri = $"https://pokeapi.co/api/v2/pokemon-species/{PokemonName}";

            using var client = new HttpClient( );
            var response = await client.GetStringAsync( uri );

            JsonNode responseNode = JsonNode.Parse( response )!;

            Pokemon pokemon = new Pokemon
            {
                Name = responseNode? [ "name" ]?.ToString( ) ?? string.Empty,
                Description = responseNode? [ "form_descriptions" ]?.AsArray( ).Count == 0 ? "" : responseNode? [ "form_descriptions" ]? [ 0 ]? [ "description" ]?.ToString( ) ?? string.Empty,
                Habitat = responseNode? [ "habitat" ]? [ "name" ]?.ToString( ) ?? string.Empty,
                IsLegendary = ( bool ) responseNode? [ "is_legendary" ]!
            };

            return pokemon;
        }

        public static async Task<string> TranslatePokemonDescription ( string description, string translationType )
        {
            using var client = new HttpClient( );

            var uri = $"https://api.funtranslations.com/translate/{translationType}.json?text={description}";
            var response = await client.GetStringAsync( uri );

            JsonNode yodaResponseNode = JsonNode.Parse( response )!;

            var translated_description = yodaResponseNode? [ "contents" ]? [ "translated" ]?.ToString( ) ?? description;

            return translated_description;
        }
    }
}

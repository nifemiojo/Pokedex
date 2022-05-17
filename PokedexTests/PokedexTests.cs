using Microsoft.AspNetCore.Mvc;
using Pokedex.Controllers;
using Pokedex.Models;
using static Pokedex.Helpers.PokemonHelpers;

namespace PokedexTests
{
	public class PokedexTests
	{
		[Test]
		public  async Task GetBasicPokemon_ReturnsCorrectPokemon_WhenPassedValidPokemon ( )
		{
			var result = await GetBasicPokemon( "pikachu" );
			var pokemon = result;

			Assert.That( pokemon.Name, Is.EqualTo( "pikachu" ) );
			Assert.That( pokemon.Description, Is.EqualTo( "" ) );
			Assert.That( pokemon.Habitat, Is.EqualTo( "forest" ) );
			Assert.That( pokemon.IsLegendary, Is.False );
		}
		
		[Test]
		public  async Task GetBasicPokemon_ReturnsNull_WhenPassedInvalidPokemon  ( )
		{
			var result = await GetBasicPokemon( "NotAPokemon" );
			var pokemon = result;

			Assert.That( pokemon, Is.Null );
		}
		
		[Test]
		public  async Task TranslatePokemonDescription_ReturnsCorrectYodaTranslation_WhenPassedLegendaryOrCavePokemon ( )
		{
			Pokemon testPokemon = new Pokemon { 
				Description = "Forms have different stats and movepools.  During evolution, Burmy's current cloak becomes Wormadam's form, and can no longer be changed.",
				Habitat = "cave",
				IsLegendary = true,
			};

			var result = await TranslatePokemonDescription( testPokemon );
			var translated = result;

			var expected = "Different stats and movepools,  forms have.During evolution,Wormadam's form,  burmy's current cloak becomes,Changed,  and can no longer be.";
			Assert.That( expected, Is.EqualTo(translated) );
		}
		
		[Test]
		public  async Task TranslatePokemonDescription_ReturnsCorrectShakespeareTranslation ( )
		{
			Pokemon testPokemon = new Pokemon { 
				Description = "Forms have different stats and movepools.  During evolution, Burmy's current cloak becomes Wormadam's form, and can no longer be changed.",
				Habitat = "anything else",
				IsLegendary = false,
			};

			var result = await TranslatePokemonDescription( testPokemon );
			var translated = result;

			var expected = "Forms hath't different stats and movepools. During evolution,  burmy's current cloak becomes wormadam's form,  and can nay longer beest did doth change.";
			Assert.That( expected, Is.EqualTo(translated) );
		}
	}
}
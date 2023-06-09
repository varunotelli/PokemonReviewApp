using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository
    {
        IList<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemonByName(string name);
        float GetPokemonRating(int id);
        bool PokemonExists(int id);
    }
}

using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        IList<Review> GetReviews();
        Review GetReview(int id);
        IList<Review> GetReviewsByPokemon(int pokemonId);
        bool ReviewExists(int id);
    }
}

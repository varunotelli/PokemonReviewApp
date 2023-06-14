using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {

        private readonly DataContext _dataContext;
        public ReviewRepository(DataContext context)
        {
            _dataContext = context;
        }
        public Review GetReview(int id)
        {
           return _dataContext.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public IList<Review> GetReviews()
        {
            return _dataContext.Reviews.ToList();
        }

        public IList<Review> GetReviewsByPokemon(int pokemonId)
        {
            return _dataContext.Reviews.Where(r => r.Pokemon.Id == pokemonId).ToList();
        }

        public bool ReviewExists(int id)
        {
            return _dataContext.Reviews.Any(r => r.Id == id);
        }
    }
}

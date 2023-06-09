using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{

    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _dataContext;
        public PokemonRepository(DataContext context)
        {
            _dataContext = context;
        }

        public Pokemon GetPokemon(int id)
        {
            return _dataContext.Pokemon.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemonByName(string name)
        {
            return _dataContext.Pokemon.Where(p => p.Name.Equals(name)).FirstOrDefault();
        }

        public float GetPokemonRating(int id)
        {
            var reviews = _dataContext.Reviews.Where(r => r.Pokemon.Id == id);
            
            return reviews.Sum(r => r.Rating) / reviews.Count();
        }

        public IList<Pokemon> GetPokemons()
        {
            return _dataContext.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int id)
        {
            return _dataContext.Pokemon.Any(p => p.Id == id);
        }
    }
}

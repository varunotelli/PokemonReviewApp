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

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _dataContext.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var category = _dataContext.Categories.Where(c  => c.Id == categoryId).FirstOrDefault();
            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon
            };
            _dataContext.Add(pokemonOwner);
            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon
            };
            _dataContext.Add(pokemonOwner);
            _dataContext.Add(pokemon);
            return Save();
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

        public bool Save()
        {
            int save = _dataContext.SaveChanges();
            return save > 0;
        }
    }
}

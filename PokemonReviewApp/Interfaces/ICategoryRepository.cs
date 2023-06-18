using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICategoryRepository
    {
        IList<Category> GetCategories();
        Category GetCategory(int id);
        IList<Pokemon> GetPokemonByCategory(int id);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);
        bool Save();

    }
}

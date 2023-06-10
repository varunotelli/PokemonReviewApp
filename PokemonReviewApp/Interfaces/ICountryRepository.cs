using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        IList<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int ownerId);
        IList<Owner> GetOwnersByCountry(int  countryId);
        bool CountryExists(int countryId);
    }
}

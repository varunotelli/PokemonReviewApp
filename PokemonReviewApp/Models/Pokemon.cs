namespace PokemonReviewApp.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public DateTime BirthDate { get; set; }
        public IList<Review> Reviews { get; set; }
        public IList<PokemonCategory> PokemonCategories { get; set; }
        public IList<PokemonOwner> PokemonOwners { get; set; }
    }
}

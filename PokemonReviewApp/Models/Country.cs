namespace PokemonReviewApp.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Owner> Owners { get; set; }
     
    }
}

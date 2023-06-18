using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;
        public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonRepository; 
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int id)
        {
            if(!_pokemonRepository.PokemonExists(id))
            {
                return NotFound();
            }

            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(id));

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(pokemon);
        }

        [HttpGet("{id}/rating")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemonRating(int id)
        {
            if (!_pokemonRepository.PokemonExists(id))
            {
                return NotFound();
            }

            var rating = _pokemonRepository.GetPokemonRating(id);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(rating);
        }

        [HttpPost]
        public IActionResult CreatePokemon([FromQuery]int ownerId, [FromQuery] int categoryId, [FromBody]PokemonDto pokemon)
        {
            if (pokemon == null) return BadRequest(ModelState);
            if (!_pokemonRepository.CreatePokemon(ownerId, categoryId, _mapper.Map<Pokemon>(pokemon))) return StatusCode(500, "Something went wrong");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok("Success!");
        }


    }
}

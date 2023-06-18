using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;
        public ReviewController(IReviewRepository reviewRepository, IPokemonRepository pokemonRepository,
            IReviewerRepository reviewerRepository,IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<IList<ReviewDto>>(_reviewRepository.GetReviews());
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public IActionResult GetReview(int id)
        {
            if(!_reviewRepository.ReviewExists(id)) { return NotFound(); }
            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(id));
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(review);
        }

        [HttpGet("pokemon/{id}")]
        public IActionResult GetReviewByPokemon(int id)
        {
            var review = _mapper.Map<IList<ReviewDto>>(_reviewRepository.GetReviewsByPokemon(id));
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(review);
        }

        [HttpPost]
        public IActionResult CreateReview([FromQuery] int pokemonId, [FromQuery] int reviewerId, [FromBody] ReviewDto review)
        {
            if (review == null)
            {
                return BadRequest(ModelState);
            }
            var mapping = _mapper.Map<Review>(review);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            mapping.Reviewer = _reviewerRepository.GetReviewer(reviewerId);
            mapping.Pokemon = _pokemonRepository.GetPokemon(pokemonId);
            if (!_reviewRepository.CreateReview(mapping))
            { return StatusCode(500, "Something went wrong while saving review"); }

            return Ok("Successfully added review!");
        }
    }
}

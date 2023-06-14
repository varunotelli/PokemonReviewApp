using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
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
    }
}

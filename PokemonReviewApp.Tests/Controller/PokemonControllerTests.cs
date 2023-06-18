using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using PokemonReviewApp.Controllers;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Tests.Controller
{
    public class PokemonControllerTests
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;
        public PokemonControllerTests()
        {
            _pokemonRepository = A.Fake<IPokemonRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void PokemonController_GetPokemons_ReturnOk()
        {
            var pokemons = A.Fake<IList<PokemonDto>>();
            var pokemonList = A.Fake<IList<PokemonDto>>();
            A.CallTo(() => _mapper.Map<IList<PokemonDto>>(pokemons)).Returns(pokemonList);
            var controller = new PokemonController(_pokemonRepository, _mapper);

            var result = controller.GetPokemons();


            result.Should().NotBeNull();
        }

        [Fact]
        public void PokemonController_CreatePokemon_ReturnOk()
        {
            int ownerId = 1;
            int catId = 2;
            var pokemondto = A.Fake<PokemonDto>();
            var pokemon = A.Fake<Pokemon>();
            A.CallTo(() => _mapper.Map<Pokemon>(pokemondto)).Returns(pokemon);
            A.CallTo(() => _pokemonRepository.CreatePokemon(ownerId, catId, pokemon)).Returns(true);
            
            var controller = new PokemonController(_pokemonRepository, _mapper);


            var result = controller.CreatePokemon(ownerId, catId, pokemondto);
            result.Should().NotBeNull();
        }

    }
}

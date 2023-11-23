using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Prologica.Api.Models;
using Prologica.Api.Services;
using Prologica.DAL.Repositories;
using apiModels = Prologica.Api.Models;                       
using dalModels = Prologica.DAL.imports;

namespace Prologica.Tests
{
    public class GameTests
    {
        private readonly IMapper mapper;
                 
        private readonly IGameRepository _gameRepository;
        private readonly GameServices _gameServices;

        public GameTests()
        {
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<apiModels.Game, dalModels.Game>();
                cfg.CreateMap<apiModels.Game, dalModels.Game>();
            }
            ).CreateMapper();

            _gameRepository = Substitute.For<IGameRepository>();

            _gameServices = new GameServices(_gameRepository, new GameValidator(), mapper);
        }

        [Fact]
        public async Task Add()
        {
            //Arrange
            var request = new apiModels.Game
            {
                Name = "game 1",
                ConsoleId = 1
            };
             _gameRepository.Add(Arg.Any<dalModels.Game>()).Returns(new dalModels.Game { Name = request.Name, Id = 1 });

            //Act
            var result = await _gameServices.Add(request);

            //Assert
            var resultObject = new OkObjectResult(result);

            Assert.Equal(200, resultObject.StatusCode);
        }

        [Fact]
        public async Task Add_invalid_request()
        {
            //Arrange

            var request = new apiModels.Game
            {
                Name = ""
            };

            //Act
            var result = await _gameServices.Add(request);

            //Assert
            var resultObject = new UnprocessableEntityObjectResult(result);

            Assert.Equal(422, resultObject.StatusCode);
        }

        [Fact]
        public async Task Update()
        {
            //Arrange
            var request = new apiModels.Game
            {
                Id = 1,
                ConsoleId = 1,
                Name = "game 1"
            };
            _gameRepository.Add(Arg.Any<dalModels.Game>()).Returns(new dalModels.Game { Name = request.Name, Id = 1 });

            //Act
            var result = await _gameServices.Update(request);

            //Assert
            var resultObject = new OkObjectResult(result);

            Assert.Equal(200, resultObject.StatusCode);
        }

        [Fact]
        public async Task Update_invalid_request()
        {
            //Arrange

            var request = new apiModels.Game
            {
                Name = ""
            };

            //Act
            var result = await _gameServices.Update(request);

            //Assert
            var resultObject = new UnprocessableEntityObjectResult(result);

            Assert.Equal(422, resultObject.StatusCode);
        }
    }
}

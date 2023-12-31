﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Prologica.Api.Models;
using Prologica.Api.Services;
using Prologica.DAL.Repositories;
using apiModels = Prologica.Api.Models;                       
using dalModels = Prologica.DAL.imports;

namespace Prologica.Tests
{
    public class ConsoleTests
    {
        private readonly IMapper mapper;
                 
        private readonly IConsoleRepository _consoleRepository;
        private readonly ConsoleServices _consoleServices;

        public ConsoleTests()
        {
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<apiModels.Console, dalModels.Console>();
                cfg.CreateMap<apiModels.Game, dalModels.Game>();
            }
            ).CreateMapper();

            _consoleRepository = Substitute.For<IConsoleRepository>();

            _consoleServices = new ConsoleServices(_consoleRepository, new ConsoleValidator(), mapper);
        }

        [Fact]
        public async Task Add()
        {
            //Arrange
            var request = new apiModels.Console
            {
                Name = "console 1"
            };
             _consoleRepository.Add(Arg.Any<dalModels.Console>()).Returns(new dalModels.Console { Name = request.Name, Id = 1 });

            //Act
            var result = await _consoleServices.Add(request);

            //Assert
            var resultObject = new OkObjectResult(result);

            Assert.Equal(200, resultObject.StatusCode);
        }

        [Fact]
        public async Task Add_invalid_request()
        {
            //Arrange

            var request = new apiModels.Console
            {
                Name = ""
            };

            //Act
            var result = await _consoleServices.Add(request);

            //Assert
            var resultObject = new UnprocessableEntityObjectResult(result);

            Assert.Equal(422, resultObject.StatusCode);
        }

        [Fact]
        public async Task Update()
        {
            //Arrange
            var request = new apiModels.Console
            {
                Id = 1,
                Name = "console 1"
            };
            _consoleRepository.Add(Arg.Any<dalModels.Console>()).Returns(new dalModels.Console { Name = request.Name, Id = 1 });

            //Act
            var result = await _consoleServices.Update(request);

            //Assert
            var resultObject = new OkObjectResult(result);

            Assert.Equal(200, resultObject.StatusCode);
        }

        [Fact]
        public async Task Update_invalid_request()
        {
            //Arrange

            var request = new apiModels.Console
            {
                Name = ""
            };

            //Act
            var result = await _consoleServices.Update(request);

            //Assert
            var resultObject = new UnprocessableEntityObjectResult(result);

            Assert.Equal(422, resultObject.StatusCode);
        }
    }
}

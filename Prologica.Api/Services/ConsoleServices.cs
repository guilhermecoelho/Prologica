using AutoMapper;
using FluentValidation;
using Prologica.DAL.Repositories;
using apiModels = Prologica.Api.Models;
using dalModels = Prologica.DAL.imports;

namespace Prologica.Api.Services
{
    public class ConsoleServices : IConsoleServices
    {
        private readonly IConsoleRepository _consoleRepository;
        private readonly IValidator<apiModels.Console> _validator;
        private readonly IMapper _mapper;

        public ConsoleServices(IConsoleRepository consoleRepository, IValidator<apiModels.Console> validator, IMapper mapper)
        {
            _consoleRepository = consoleRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<IResult> Add(apiModels.Console request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (validationResult.IsValid == false)
                return Results.UnprocessableEntity(validationResult.ToDictionary());

            var result = await _consoleRepository.Add(_mapper.Map<dalModels.Console>(request));

            return Results.Ok(result);
        }

        public async Task<IResult> Update(apiModels.Console request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (validationResult.IsValid == false)
                return Results.UnprocessableEntity(validationResult.ToDictionary());

            var result = await _consoleRepository.Update(_mapper.Map<dalModels.Console>(request));

            return Results.Ok(result);
        }

        public async Task<IResult> GetAll() =>
            Results.Ok(await _consoleRepository.GetAll());

        public async Task<IResult> GetById(int id) =>
           Results.Ok(await _consoleRepository.GetById(id));

        public async Task<IResult> Delete(int id)
        {
            var model = await _consoleRepository.GetById(id);
            if (model == null)
                return Results.UnprocessableEntity(model);

            await _consoleRepository.Remove(model);

            return Results.Ok();
        }
    }
}

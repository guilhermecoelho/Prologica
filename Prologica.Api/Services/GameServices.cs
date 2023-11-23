using AutoMapper;
using FluentValidation;
using Prologica.DAL.Repositories;
using apiModels = Prologica.Api.Models;
using dalModels = Prologica.DAL.imports;

namespace Prologica.Api.Services
{
    public class GameServices : IGameServices
    {
        private readonly IGameRepository _GameRepository;
        private readonly IValidator<apiModels.Game> _validator;
        private readonly IMapper _mapper;

        public GameServices(IGameRepository GameRepository, IValidator<apiModels.Game> validator, IMapper mapper)
        {
            _GameRepository = GameRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<IResult> Add(apiModels.Game request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (validationResult.IsValid == false)
                return Results.UnprocessableEntity(validationResult.ToDictionary());

            var result = await _GameRepository.Add(_mapper.Map<dalModels.Game>(request));

            return Results.Ok(result);
        }

        public async Task<IResult> Update(apiModels.Game request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (validationResult.IsValid == false)
                return Results.UnprocessableEntity(validationResult.ToDictionary());

            var result = await _GameRepository.Update(_mapper.Map<dalModels.Game>(request));

            return Results.Ok(result);
        }

        public async Task<IResult> GetAll() =>
            Results.Ok(await _GameRepository.GetAll());

        public async Task<IResult> GetById(int id) =>
           Results.Ok(await _GameRepository.GetById(id));

        public async Task<IResult> Delete(int id)
        {
            var model = await _GameRepository.GetById(id);
            if (model == null)
                return Results.UnprocessableEntity(model);

            await _GameRepository.Remove(model);

            return Results.Ok();
        }
    }
}

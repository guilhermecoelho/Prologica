using Prologica.Api.Models;

namespace Prologica.Api.Services
{
    public interface IGameServices
    {
        Task<IResult> Add(Game request);
        Task<IResult> Delete(int id);
        Task<IResult> GetAll();
        Task<IResult> GetById(int id);
        Task<IResult> Update(Game request);
    }
}
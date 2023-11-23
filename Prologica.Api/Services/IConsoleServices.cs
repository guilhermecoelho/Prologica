namespace Prologica.Api.Services
{
    public interface IConsoleServices
    {
        Task<IResult> Add(Models.Console request);
        Task<IResult> Update(Models.Console request);
        Task<IResult> GetAll();
        Task<IResult> GetById(int id);
        Task<IResult> Delete(int id);
    }
}
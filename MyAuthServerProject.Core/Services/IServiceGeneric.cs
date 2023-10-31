using SharedLibrary.Dtos;
using System.Linq.Expressions;

namespace MyAuthServerProject.Core.Services
{
    public interface IServiceGeneric<TKey, TDto> where TDto : class where TKey : class
    {
        Task<Response<TDto>> GetByIdAsync(int id);
        Task<Response<IEnumerable<TDto>>> GetByFilterAsync(Expression<Func<TKey, bool>> predicate);
        //IEnumerable önce tüm veriyi belleğe alıp sonrasında istenilen veriyi getirir. IQuareyable ise istenilen verileri direkt veritabanı sorgusu gibi getirir.
        Task<Response<IEnumerable<TDto>>> GetAllAsync();
        Task<Response<TDto>> AddAsync(TDto dto);
        Task<Response<NoContent>> RemoveAsync(int id);
        Task<Response<NoContent>> UpdateAsync(TDto dto, int id);
    }
}

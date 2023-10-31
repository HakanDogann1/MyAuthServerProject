using MyAuthServerProject.Core.Repositories;
using MyAuthServerProject.Core.Services;
using MyAuthServerProject.Core.UnitOfWork;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyAuthServerProject.Service.Services
{
    public class GenericService<TEntity, TDto> : IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public GenericService(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<TDto>> AddAsync(TDto dto)
        {
            await _repository.AddAsync(ObjectMapper.Mapper.Map<TEntity>(dto));
            await _unitOfWork.CommitAsync();
            return Response<TDto>.Success(dto, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var values =await _repository.GetAllAsync();
            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<List<TDto>>(values), 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var value = _repository.GetByFilter(predicate);
            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(value), 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var value = await _repository.GetByIdAsync(id);
            if(value != null)
            {
                return Response<TDto>.Fail("Bulunamadı", 404, true);
            }
            return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(value), 200);
        }

        public async Task<Response<NoContent>> RemoveAsync(int id)
        {
            var value = await _repository.GetByIdAsync(id);
            if(value != null)
            {
                return Response<NoContent>.Fail("Kullanıcı Bulunamadı", 404, true);
            }
            _repository.Remove(value);
            await _unitOfWork.CommitAsync();
            return Response<NoContent>.Success(200);
        }

        public async Task<Response<NoContent>> UpdateAsync(TDto dto, int id)
        {
            var value = await _repository.GetByIdAsync(id);
            if(value == null)
            {
                return Response<NoContent>.Fail("Id bulunamadı", 404, true);
            }
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(dto);
             _repository.Update(newEntity);
            await _unitOfWork.CommitAsync();
            return Response<NoContent>.Success(200);
        }
    }
}

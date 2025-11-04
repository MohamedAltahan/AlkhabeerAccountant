using Alkhabeer.Core.Shared;
using Alkhabeer.Data.Repositories;
using System.Diagnostics;


namespace Alkhabeer.Service.Base
{
    public class BaseService<T> where T : class
    {
        protected readonly BaseRepository<T> _repository;

        public BaseService(BaseRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<Result<T>> AddAsync(T entity)
        {
            try
            {
                await _repository.AddAsync(entity);
                return Result<T>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result<T>.Failure($"خطأ أثناء الإضافة: {ex.Message}");
            }
        }

        public virtual async Task<Result<T>> UpdateAsync(T entity)
        {
            try
            {
                await _repository.UpdateAsync(entity);
                return Result<T>.Success(entity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return Result<T>.Failure($"خطأ أثناء التحديث: {ex.Message}");
            }
        }

        public virtual async Task<Result> DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return Result.Success();
            }
            catch
            {
                return Result.Failure();
            }
        }

        public virtual async Task<PaginatedResult<T>> GetPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _repository.GetPagedAsync(pageNumber, pageSize);
                return result;
            }
            catch
            {
                return new PaginatedResult<T>(new List<T>(), 0, 0, pageSize, "حدث خطأ ما");
            }
        }

    }
}

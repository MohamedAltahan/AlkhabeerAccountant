using Alkhabeer.Core.Shared;
using Alkhabeer.Data.Repositories;

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
            catch (Exception ex)
            {
                return Result.Failure($"خطأ أثناء الحذف: {ex.Message}");
            }
        }

        public virtual async Task<PaginatedResult<T>> GetPagedAsync(int page, int size)
        {
            try
            {
                // The repository already returns a PaginatedResult<T>
                var result = await _repository.GetPagedAsync(page, size);
                return result;
            }
            catch (Exception ex)
            {
                // In case of error, return an empty paginated result with an error message
                return new PaginatedResult<T>(
                    data: new List<T>(),
                    totalCount: 0,
                    pageNumber: 0,
                    pageSize: size
                );
            }
        }

    }
}

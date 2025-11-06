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
            catch (Exception ex)
            {
                {
                    Debug.WriteLine(ex.ToString());
                    return Result.Failure(ex.ToString());
                }
            }
        }

        public virtual async Task<Result<List<T>>> GetAllAsync()
        {
            try
            {
                var result = await _repository.GetAllAsync();

                return Result<List<T>>.Success(result);
            }
            catch
            {
                return Result<List<T>>.Failure();
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

        public virtual async Task<Result<T>> SaveOrUpdateAsync(T entity)
        {
            try
            {
                // Use reflection to read the "Id" property of the entity
                var idProperty = typeof(T).GetProperty("Id");

                var idValue = idProperty.GetValue(entity);

                // Handle both int and long IDs safely
                bool isNew = idValue == null || (idValue is int intId && intId == 0);

                if (isNew)
                    return await AddAsync(entity);
                else
                    return await UpdateAsync(entity);
            }
            catch
            {
                return Result<T>.Failure();
            }
        }


    }
}

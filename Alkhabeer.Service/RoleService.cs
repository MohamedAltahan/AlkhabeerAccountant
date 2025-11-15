using Alkhabeer.Core.Models;
using Alkhabeer.Core.Shared;
using Alkhabeer.Data;
using Alkhabeer.Data.Repositories;
using Alkhabeer.Service.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Alkhabeer.Service
{
    public class RoleService : BaseService<Role>
    {
        private new readonly RoleRepository _repository;
        private readonly DBContext _context;
        public RoleService(RoleRepository repository, DBContext context) : base(repository)
        {
            _repository = repository;
            _context = context;
        }
        public async Task<Result> SaveOrUpdateWithPermissionsAsync(Role role, List<int> permissionIds)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {

                await _repository.SaveRoleAsync(role);

                await _repository.RemoveRolePermissionsAsync(role.Id);//  Remove old permissions 
                if (permissionIds != null && (permissionIds.Count() > 0))//  Add new permissions
                    await _repository.AddRolePermissionsAsync(role.Id, permissionIds);

                await transaction.CommitAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result.Failure($"Error saving role: {ex.Message}");
            }
        }
        public Task<List<Role>> GetAllWithPermissionsAsync()
        {
            return _repository.GetAllWithPermissionsAsync();
        }

        public Task<Role?> GetByIdWithPermissionsAsync(int id)
        {
            return _repository.GetByIdWithPermissionsAsync(id);
        }

        public Task<List<Permission>> GetAllPermissionsAsync()
        {
            return _repository.GetAllPermissionsAsync();
        }

        public Task<PaginatedResult<Role>> GetPagedWithPermissionsAsync(int page, int pageSize)
        {
            return _repository.GetPagedWithPermissionsAsync(page, pageSize);
        }

        public async Task<Result> AddRoleAsync(Role role, IEnumerable<int>? permissions)
        {
            try
            {
                await _repository.AddWithPermissionsAsync(role, permissions);
                return Result.Success();
            }
            catch (System.Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> UpdateRoleAsync(Role role, IEnumerable<int>? permissions)
        {
            try
            {
                await _repository.UpdateWithPermissionsAsync(role, permissions);
                return Result.Success();
            }
            catch (System.Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }




    }
}

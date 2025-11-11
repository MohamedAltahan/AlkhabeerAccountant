using Alkhabeer.Core.Models;
using Alkhabeer.Core.Shared;
using Alkhabeer.Data.Repositories;
using Alkhabeer.Service.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkhabeer.Services
{
    public class UserService : BaseService<User>
    {
        private readonly UserRepository _userRepo;
        private readonly RoleRepository _roleRepo;

        public UserService(UserRepository userRepo, RoleRepository roleRepo) : base(userRepo)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }

        public async Task<Result<List<User>>> GetAllWithRolesAsync()
        {

            var users = await _userRepo.GetAllWithRolesAsync();
            return Result<List<User>>.Success(users);
        }

        public async Task<Result<PaginatedResult<User>>> GetPagedWithRolesAsync(int page, int pageSize)
        {
            var result = await _userRepo.GetPagedWithRolesAsync(page, pageSize);
            return Result<PaginatedResult<User>>.Success(result);
        }

        public async Task<Result<List<Role>>> GetRolesAsync()
        {
            var roles = await _roleRepo.GetAllWithPermissionsAsync();
            return Result<List<Role>>.Success(roles);
        }



        // ✅ Delete user
        public async Task<Result> DeleteAsync(int id)
        {
            await _userRepo.DeleteAsync(id);
            return Result.Success();
        }

        // ✅ Assign roles manually
        //public async Task<Result> AssignRolesAsync(int userId, IEnumerable<int> roleIds)
        //{
        //    await _userRepo.AssignRolesAsync(userId, roleIds);
        //    return Result.Success();
        //}

        // ✅ Get roles for specific user
        //public async Task<Result<List<Role>>> GetUserRolesAsync(int userId)
        //{
        //    var roles = await _userRepo.GetRolesForUserAsync(userId);
        //    return Result<List<Role>>.Success(roles);
        //}
    }
}

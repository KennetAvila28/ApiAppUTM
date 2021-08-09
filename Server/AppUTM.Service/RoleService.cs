#region Using

using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using AppUTM.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion Using

namespace AppUTM.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;

        public RoleService(IUnitOfWork unitOfWork, DataContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<Role> CreateRole(Role newRole)
        {
            await _unitOfWork.Roles.Add(newRole);
            await _unitOfWork.RolePermission.AddRange(newRole.RolePermissions);
            await _unitOfWork.CommitAsync();
            return newRole;
        }

        public async Task<IEnumerable<Role>> GetAllRoles() => await _context.Roles.Include(x => x.RolePermissions).ToListAsync();

        public async Task<Role> GetRoleById(int id) =>
            await _context.Roles.Include(x => x.RolePermissions).ThenInclude(y => y.Permission)
                .SingleOrDefaultAsync(z => z.Id == id);

        public async Task UpdateRole(Role roleToBeUpdated)
        {
            roleToBeUpdated.UpdateAt = DateTime.Now;
            var rolePermissionRemove = await _context.RolePermissions.Where(x => x.RoleId == roleToBeUpdated.Id).ToListAsync();
            _unitOfWork.RolePermission.RemoveRange(rolePermissionRemove);
            await _unitOfWork.RolePermission.AddRange(roleToBeUpdated.RolePermissions);
            _unitOfWork.Roles.Update(roleToBeUpdated);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRole(Role role)
        {
            _unitOfWork.RolePermission.RemoveRange(role.RolePermissions);
            _unitOfWork.Roles.Remove(role);
            await _unitOfWork.CommitAsync();
        }

        //public async Task<List<NavigationMenu>> GetPermissionsByRoleIdAsync(string id)
        //{
        //    var items = await (from m in _context.NavigationMenu
        //            join rm in _context.RoleMenuPermission
        //                on new { X1 = m.Id, X2 = id } equals
        //                new { X1 = rm.NavigationMenuId, X2 = rm.RoleId }
        //                into rmp
        //            from rm in rmp.DefaultIfEmpty()
        //            select new NavigationMenuViewModel()
        //            {
        //                Id = m.Id,
        //                Name = m.Name,
        //                Area = m.Area,
        //                ActionName = m.ActionName,
        //                ControllerName = m.ControllerName,
        //                IsExternal = m.IsExternal,
        //                ExternalUrl = m.ExternalUrl,
        //                DisplayOrder = m.DisplayOrder,
        //                ParentMenuId = m.ParentMenuId,
        //                Visible = m.Visible,
        //                Permitted = rm.RoleId == id
        //            })
        //        .AsNoTracking()
        //        .ToListAsync();

        //    return items;
        //}
        //public async Task<bool> SetPermissionsByRoleIdAsync(string id, IEnumerable<Guid> permissionIds)
        //{
        //    var existing = await _context.RoleMenuPermission.Where(x => x.RoleId == id).ToListAsync();
        //    _context.RemoveRange(existing);

        //    foreach (var item in permissionIds)
        //    {
        //        await _context.RoleMenuPermission.AddAsync(new RoleMenuPermission()
        //        {
        //            RoleId = id,
        //            NavigationMenuId = item,
        //        });
        //    }

        //    var result = await _context.SaveChangesAsync();

        //    // Remove existing permissions to roles from Cache so it can re evaluate and take effect
        //    _cache.Remove("RolePermissions");

        //    return result > 0;
        //}
    }
}
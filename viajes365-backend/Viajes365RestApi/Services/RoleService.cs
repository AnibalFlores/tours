using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajes365RestApi.Entities;
using Viajes365RestApi.Handlers;
using Viajes365RestApi.Helpers;

namespace Viajes365RestApi.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetById(long id);
        Role Create(Role role);
        void Update(Role role);
        void Delete(long id);
    }

    public class RoleService : IRoleService
    {
        private DataContext _context;

        public RoleService(DataContext context)
        {
            _context = context;
        }

        Role IRoleService.Create(Role role)
        {
            // validation
            if (string.IsNullOrWhiteSpace(role.RoleName))
                throw new AppException("RoleName is required");

            /*if (string.IsNullOrWhiteSpace(role.RoleType.ToString()))
                throw new AppException("RoleType is required");*/

            role.Active = true;
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        void IRoleService.Delete(long id)
        {
            var role = _context.Roles.Find(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }

        IEnumerable<Role> IRoleService.GetAll()
        {
            return _context.Roles.ToList();
        }

        Role IRoleService.GetById(long id)
        {
            return _context.Roles.Find(id);
        }

        void IRoleService.Update(Role roleParam)
        {
            var role = _context.Roles.Find(roleParam.RoleId);

            if (role == null)
                throw new AppException("Role not found");

            // update rolename if it has changed
            if (!string.IsNullOrWhiteSpace(roleParam.RoleName) && roleParam.RoleName != role.RoleName)
            {
                // throw error if the new username is already taken
                if (_context.Roles.Any(x => x.RoleName == roleParam.RoleName))
                    throw new AppException("Rolename " + roleParam.RoleName + " already exists");

                role.RoleName = roleParam.RoleName;
            }

            // update role type if provided
            /* if (!string.IsNullOrWhiteSpace(roleParam.RoleType.ToString()))
                 role.RoleType = roleParam.RoleType;*/

            if (!string.IsNullOrWhiteSpace(roleParam.Active.ToString()))
                role.Active = roleParam.Active;

            /*if (roleParam.Users.Count > 0)
                role.Users = roleParam.Users;*/

            // saving updates
            _context.Roles.Update(role);
            _context.SaveChanges();
        }
    }
}

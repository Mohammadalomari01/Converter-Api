using Converter.Core.Models;
using Converter.Core.Repository;
using Converter.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Infra.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // Get all roles
        public List<Role> GetAllRoles()
        {
            return _roleRepository.GetAllRoles();
        }

        // Get a role by ID
        public Role GetRoleById(int id)
        {
            return _roleRepository.GetRoleById(id);
        }

        // Create a new role
        public void CreateRole(Role role)
        {
            // Add any additional validation logic here if necessary
            _roleRepository.CreateRole(role);
        }

        // Update an existing role
        public void UpdateRole(Role role)
        {
            // Add any additional validation logic here if necessary
            _roleRepository.UpdateRole(role);
        }

        // Delete a role by ID
        public void DeleteRole(int id)
        {
            _roleRepository.DeleteRole(id);
        }
    }
}

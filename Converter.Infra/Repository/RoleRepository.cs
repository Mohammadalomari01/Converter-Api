using Converter.Core.Common;
using Converter.Core.Models;
using Converter.Core.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Infra.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbContext _dbContext;

        public RoleRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Retrieve all roles
        public List<Role> GetAllRoles()
        {
            IEnumerable<Role> result = _dbContext.Connection.Query<Role>(
                "Role_Package.GetAllRoles",
                commandType: CommandType.StoredProcedure
            );

            return result.ToList();
        }

        // Retrieve a role by ID
        public Role GetRoleById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_RoleId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var result = _dbContext.Connection.Query<Role>(
                "Role_Package.GetRoleById",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result.SingleOrDefault();
        }

        // Create a new role
        public void CreateRole(Role role)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_RoleName", role.Rolename, dbType: DbType.String, direction: ParameterDirection.Input);

            _dbContext.Connection.Execute(
                "Role_Package.CreateRole",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        // Update an existing role
        public void UpdateRole(Role role)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_RoleId", role.Roleid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameters.Add("p_RoleName", role.Rolename, dbType: DbType.String, direction: ParameterDirection.Input);

            _dbContext.Connection.Execute(
                "Role_Package.UpdateRole",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        // Delete a role
        public void DeleteRole(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_RoleId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            _dbContext.Connection.Execute(
                "Role_Package.DeleteRole",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}

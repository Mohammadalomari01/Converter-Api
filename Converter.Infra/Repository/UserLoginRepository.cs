using Converter.Core.Common;
using Converter.Core.Models;
using Converter.Core.Repository;
using Converter.Core.Services;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Infra.Repository
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;

        public UserLoginRepository(IDbContext dbContext, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        // Retrieve all UserLogins
        public List<Userlogin> GetAllUserLogins()
        {
            IEnumerable<Userlogin> result = _dbContext.Connection.Query<Userlogin>("UserLogin_Package.GetAllUserLogins", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        // Retrieve a single UserLogin by ID
        public Userlogin GetUserLoginById(int id)
        {
            var p = new DynamicParameters();
            p.Add("p_UserLoginId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var result = _dbContext.Connection.Query<Userlogin>("UserLogin_Package.GetUserLoginById", p, commandType: CommandType.StoredProcedure);
            return result.SingleOrDefault();
        }

        // Create a new UserLogin
        public void CreateUserLogin(Userlogin userLogin)
        {
            var p = new DynamicParameters();
            p.Add("p_Email", userLogin.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_Password", _passwordHasher.Hash(userLogin.Password), dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_RoleId", userLogin.Roleid, dbType: DbType.Int32, direction: ParameterDirection.Input);

            _dbContext.Connection.Execute("UserLogin_Package.CreateUserLogin", p, commandType: CommandType.StoredProcedure);
        }

        // Update an existing UserLogin
        public void UpdateUserLogin(Userlogin userLogin)
        {
            var p = new DynamicParameters();
            p.Add("p_UserLoginId", userLogin.Userloginid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_Email", userLogin.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_Password", _passwordHasher.Hash(userLogin.Password), dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_RoleId", userLogin.Roleid, dbType: DbType.Int32, direction: ParameterDirection.Input);

            _dbContext.Connection.Execute("UserLogin_Package.UpdateUserLogin", p, commandType: CommandType.StoredProcedure);
        }

        // Delete a UserLogin by ID
        public void DeleteUserLogin(int id)
        {
            var p = new DynamicParameters();
            p.Add("p_UserLoginId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            _dbContext.Connection.Execute("UserLogin_Package.DeleteUserLogin", p, commandType: CommandType.StoredProcedure);
        }

        // Authenticate a UserLogin
        public Userlogin Auth(Userlogin userLogin)
        {
            var p = new DynamicParameters();
            p.Add("p_Email", userLogin.Email, dbType: DbType.String, direction: ParameterDirection.Input);

            var result = _dbContext.Connection.Query<Userlogin>("UserLogin_Package.Authenticate", p, commandType: CommandType.StoredProcedure).FirstOrDefault();

            if (result == null)
            {
                return null;
            }

            bool isPasswordValid = _passwordHasher.Verify(result.Password, userLogin.Password);
            return isPasswordValid ? result : null;
        }

       
    }
}

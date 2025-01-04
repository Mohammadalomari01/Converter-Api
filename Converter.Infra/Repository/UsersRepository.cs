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
    public class UsersRepository: IUsersRepository
    {
        private readonly IDbContext _dbContext;

        public UsersRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Method to retrieve all users
        public List<User> GetAllUsers()
        {
            IEnumerable<User> result = _dbContext.Connection.Query<User>("User_Package.GetAllUsers", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        // Method to retrieve a user by ID
        public User GetUserById(int id)
        {
            var p = new DynamicParameters();
            p.Add("p_userId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var result = _dbContext.Connection.Query<User>("User_Package.GetUserById", p, commandType: CommandType.StoredProcedure);
            return result.SingleOrDefault();
        }

        // Method to create a new user
        public void CreateUser(User user)
        {
            var p = new DynamicParameters();
            p.Add("p_fullName", user.Fullname, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_PhoneNumber", user.Phonenumber, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_userLoginId", user.Userloginid, dbType: DbType.Int32, direction: ParameterDirection.Input);

            _dbContext.Connection.Execute("User_Package.CreateUser", p, commandType: CommandType.StoredProcedure);
        }

        // Method to update an existing user
        public void UpdateUser(User user)
        {
            var p = new DynamicParameters();
            p.Add("p_userId", user.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_fullName", user.Fullname, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_PhoneNumber", user.Phonenumber, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_userLoginId", user.Phonenumber, dbType: DbType.Int32, direction: ParameterDirection.Input);

            _dbContext.Connection.Execute("User_Package.UpdateUser", p, commandType: CommandType.StoredProcedure);
        }

        // Method to delete a user
        public void DeleteUser(int id)
        {
            var p = new DynamicParameters();
            p.Add("p_userId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            _dbContext.Connection.Execute("User_Package.DeleteUser", p, commandType: CommandType.StoredProcedure);
        }
    }
}


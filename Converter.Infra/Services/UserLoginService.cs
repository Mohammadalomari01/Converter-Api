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
    public class UserLoginService: IUserLoginService
    {
        private readonly IUserLoginRepository _userLoginRepository;

        public UserLoginService(IUserLoginRepository userLoginRepository)
        {
            _userLoginRepository = userLoginRepository;
        }

        // Retrieve all UserLogins
        public List<Userlogin> GetAllUserLogins()
        {
            return _userLoginRepository.GetAllUserLogins();
        }

        // Retrieve a specific UserLogin by ID
        public Userlogin GetUserLoginById(int id)
        {
            return _userLoginRepository.GetUserLoginById(id);
        }

        // Create a new UserLogin
        public void CreateUserLogin(Userlogin userLogin)
        {
            if (userLogin == null) throw new ArgumentNullException(nameof(userLogin));
            _userLoginRepository.CreateUserLogin(userLogin);
        }

        // Update an existing UserLogin
        public void UpdateUserLogin(Userlogin userLogin)
        {
            if (userLogin == null || userLogin.Userloginid <= 0) throw new ArgumentException("Invalid UserLogin");
            _userLoginRepository.UpdateUserLogin(userLogin);
        }

        // Delete a UserLogin by ID
        public void DeleteUserLogin(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ID");
            _userLoginRepository.DeleteUserLogin(id);
        }

        // Authenticate a UserLogin (login functionality)
        public Userlogin Auth(Userlogin userLogin)
        {
            if (userLogin == null || string.IsNullOrEmpty(userLogin.Email) || string.IsNullOrEmpty(userLogin.Password))
                throw new ArgumentException("Invalid login credentials");

            return _userLoginRepository.Auth(userLogin);
        }

       
      
    }
}


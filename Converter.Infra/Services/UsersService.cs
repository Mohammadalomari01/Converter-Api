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
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;

        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Retrieve all users
        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        // Retrieve a user by their ID
        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        // Create a new user
        public void CreateUser(User user)
        {
            _userRepository.CreateUser(user);
        }

        // Update an existing user
        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }

        // Delete a user by ID
        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }
    }
}


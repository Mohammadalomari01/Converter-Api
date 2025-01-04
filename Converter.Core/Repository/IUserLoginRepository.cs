using Converter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Core.Repository
{
    public interface IUserLoginRepository
    {
        List<Userlogin> GetAllUserLogins();
        Userlogin GetUserLoginById(int id);
        void CreateUserLogin(Userlogin userLogin);
        void UpdateUserLogin(Userlogin userLogin);
        void DeleteUserLogin(int id);
        Userlogin Auth(Userlogin userLogin);
    }
}

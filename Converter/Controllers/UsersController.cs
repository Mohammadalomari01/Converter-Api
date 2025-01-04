using Converter.Core.Models;
using Converter.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Converter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       
            private readonly IUsersService _userService;

            public UsersController(IUsersService userService)
            {
                _userService = userService;
            }

            [HttpGet]
            //[CheckClaims("roleid", "21")]
            public List<User> GetAllUsers()
            {
                return _userService.GetAllUsers();
            }

            [HttpGet]
            [Route("GetUserById")]
            public ActionResult<User> GetUserById()
            {
                var userId = User.FindFirstValue("userid");
                if (userId == null)
                {
                    return BadRequest();
                }
                return Ok(_userService.GetUserById(int.Parse(userId)));
            }

            [HttpPost]
            [Route("CreateUser")]
            public void CreateUser(User user)
            {
                _userService.CreateUser(user);
            }

            [HttpPut]
            [Route("UpdateUser")]
            //[CheckClaims("roleid", "1")]
            public void UpdateUser(User user)
            {
                _userService.UpdateUser(user);
            }

            [HttpDelete]
            [Route("DeleteUser/{id}")]
            public void DeleteUser(int id)
            {
                _userService.DeleteUser(id);
            }
        }
    }


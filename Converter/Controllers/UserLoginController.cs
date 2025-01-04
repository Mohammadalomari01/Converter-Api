using Converter.Core.Models;
using Converter.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace Converter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IUserLoginService _userLoginService;

        public UserLoginController(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }

        // Get all UserLogins
        [HttpGet]
        //[CheckClaims("roleid", "21")]
        
        public List<Userlogin> GetAllUserLogins()
        {
            return _userLoginService.GetAllUserLogins();
        }

        // Get a UserLogin by ID
        [HttpGet]
        [Route("GetUserLoginById/{id}")]
        public Userlogin GetUserLoginById(int id)
        {
            return _userLoginService.GetUserLoginById(id);
        }

        // Create a new UserLogin
        [HttpPost]
        [Route("CreateUserLogin")]
        public IActionResult CreateUserLogin(Userlogin userLogin)
        {
            try
            {
                _userLoginService.CreateUserLogin(userLogin);
                return Ok();
            }
            catch (OracleException ex) when (ex.Number == 1) 
            {
                return BadRequest("Email is already in use!");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // Update an existing UserLogin
        [HttpPut]
        [Route("UpdateUserLogin")]
        //[CheckClaims("roleid", "21")]
        [Authorize]
        public IActionResult UpdateUserLogin(Userlogin userLogin)
        {
            try
            {
                _userLoginService.UpdateUserLogin(userLogin);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // Delete a UserLogin by ID
        [HttpDelete]
        [Route("DeleteUserLogin/{id}")]
        //[CheckClaims("roleid", "21")]
        [Authorize]
        public IActionResult DeleteUserLogin(int id)
        {
            try
            {
                _userLoginService.DeleteUserLogin(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // Authenticate UserLogin
        [HttpPost]
        [Route("Login")]
        public IActionResult Auth(Userlogin userLogin)
        {
            try
            {
                var token = _userLoginService.Auth(userLogin);
                if (token == null)
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(token);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Invalid email/password: {ex.Message}");
            }
        }

        
    }
}


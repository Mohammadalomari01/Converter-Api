using Converter.Core.Models;
using Converter.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Converter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/Role
        [HttpGet]
        //[CheckClaims("roleid", "21")]
        public ActionResult<List<Role>> GetAllRoles()
        {
            var roles = _roleService.GetAllRoles();
            return Ok(roles);
        }

        // GET: api/Role/GetRoleById/{id}
        [HttpGet]
        [Route("GetRoleById/{id}")]
        //[CheckClaims("roleid", "21")]
        public ActionResult<Role> GetRoleById(int id)
        {
            var role = _roleService.GetRoleById(id);
            if (role == null)
            {
                return NotFound($"Role with ID {id} not found.");
            }
            return Ok(role);
        }

        // POST: api/Role/CreateRole
        [HttpPost]
        [Route("CreateRole")]
        //[CheckClaims("roleid", "21")]
        public IActionResult CreateRole(Role role)
        {
            _roleService.CreateRole(role);
            return CreatedAtAction(nameof(GetRoleById), new { id = role.Roleid }, role);
        }

        // PUT: api/Role/UpdateRole
        [HttpPut]
        [Route("UpdateRole")]
        //[CheckClaims("roleid", "21")]
        public IActionResult UpdateRole(Role role)
        {
            var existingRole = _roleService.GetRoleById((int)role.Roleid);
            if (existingRole == null)
            {
                return NotFound($"Role with ID {role.Roleid} not found.");
            }

            _roleService.UpdateRole(role);
            return NoContent();
        }

        // DELETE: api/Role/DeleteRole/{id}
        [HttpDelete]
        [Route("DeleteRole/{id}")]
        //[CheckClaims("roleid", "21")]
        public IActionResult DeleteRole(int id)
        {
            var role = _roleService.GetRoleById(id);
            if (role == null)
            {
                return NotFound($"Role with ID {id} not found.");
            }

            _roleService.DeleteRole(id);
            return NoContent();
        }
    }
}

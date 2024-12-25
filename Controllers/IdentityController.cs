using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using TaoyuanBIMAPI.Parameter;
using TaoyuanBIMAPI.Repository.Interface;

namespace TaoyuanBIMAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private IIdentityRepository _identityRepository;
        public IdentityController(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult Register([FromBody] RegisterParameter registerParamter)
        {
            return Ok(_identityRepository.Register(registerParamter));
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult<object> Login([FromBody] LoginPrarmeter loginPrarmeter)
        {
            return Ok(_identityRepository.Login(loginPrarmeter));
        }
        [HttpPost]
        [Route("CreateRole")]
        public ActionResult CreateRole([FromBody] CreateRoleParameter createRoleParameter)
        {
            return Ok(_identityRepository.CreateRole(createRoleParameter));
        }
        [HttpPost]
        [Route("AssignRole")]
        public ActionResult AssignRole([FromBody] AssignRoleParameter assignRoleParameter)
        {
            return Ok(_identityRepository.AssignRole(assignRoleParameter));
        }
    }
}

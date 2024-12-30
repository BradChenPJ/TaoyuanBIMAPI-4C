using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using TaoyuanBIMAPI.Parameter;
using TaoyuanBIMAPI.Repository.Interface;
using TaoyuanBIMAPI.ViewModel;

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
            dynamic loginResponse = _identityRepository.Login(loginPrarmeter);
            if (loginResponse.Status == true)
            {
                // return 時設定將一些資訊直接存入HTTP-only cookie
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true, // Set to true in production
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(8).AddMinutes(30) // 設定過期時間，跟token一樣
                };
                HttpContext.Response.Cookies.Append("AuthToken", loginResponse.Token, cookieOptions);
                return Ok(new {Status = loginResponse.Status, Message = loginResponse.Message });
            }
            else 
            {
                return Unauthorized(loginResponse);
            }


            
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

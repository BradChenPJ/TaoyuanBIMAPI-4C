using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaoyuanBIMAPI.Model.Data;
using TaoyuanBIMAPI.Model.Identity;
using TaoyuanBIMAPI.Parameter;
using TaoyuanBIMAPI.Repository.Interface;
using TaoyuanBIMAPI.ViewModel;

namespace TaoyuanBIMAPI.Repository.Implement
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IConfiguration _configuration;
        private readonly TaoyuanBimIdentityContext _taoyuanBimIdentityContext;
        private readonly UserManager<TaoyuanBIMUser> _userManager;
        private readonly RoleManager<TaoyuanBIMRole> _roleManager;

        public IdentityRepository(IConfiguration configuration, TaoyuanBimIdentityContext taoyuanBimIdentityContext, UserManager<TaoyuanBIMUser> userManager, RoleManager<TaoyuanBIMRole> roleManager)
        {
            _configuration = configuration;
            _taoyuanBimIdentityContext = taoyuanBimIdentityContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ResponseViewModel Register(RegisterParameter registerParameter)
        {
            ResponseViewModel _responseViewModel = new ResponseViewModel();
            TaoyuanBIMUser _user = _userManager.FindByNameAsync(registerParameter.UserName).Result;  //不用async await 就是用.Result
            TaoyuanBIMUser _email = _userManager.FindByEmailAsync(registerParameter.Email).Result;
            if (_user != null)
            {
                _responseViewModel.Status = false;
                _responseViewModel.Message = "此帳號已被註冊";
                return _responseViewModel;
            }
            if (_email != null)
            {
                _responseViewModel.Status = false;
                _responseViewModel.Message = "此email已被註冊";
                return _responseViewModel;
            }
            _user = new TaoyuanBIMUser
            {
                UserName = registerParameter.UserName,
                Email = registerParameter.Email,
                CName = registerParameter.CName,
                Department = registerParameter.Department,
                Title = registerParameter.Title,
                Phone = registerParameter.Phone,
                Availablility = true
            };
            var result = _userManager.CreateAsync(_user, registerParameter.Password).Result;
            if (result.Succeeded)
            {
                _responseViewModel.Status = true;
                _responseViewModel.Message = "註冊成功";
                return _responseViewModel;
            }
            else
            {
                string errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                _responseViewModel.Status = false;
                _responseViewModel.Message = errorMessage;
                return _responseViewModel;
            }
        }
        public object Login(LoginPrarmeter loginPrarmeter)
        {
            ResponseViewModel _responseViewModel = new ResponseViewModel();
            TaoyuanBIMUser _user = _userManager.FindByNameAsync(loginPrarmeter.UserName).Result;
            if (_user == null)
            {
                _responseViewModel.Status = false;
                _responseViewModel.Message = "此帳號不存在";
                return _responseViewModel;
            }
            var result = _userManager.CheckPasswordAsync(_user, loginPrarmeter.Password).Result;
            if (!result)
            {
                _responseViewModel.Status = false;
                _responseViewModel.Message = "密碼錯誤";
                return _responseViewModel;
            }
            else
            {
                // token claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _user.UserName),
                    new Claim("CName", _user.CName),
                    new Claim("Department", _user.Department)
                };
                List<string> roleNames = _userManager.GetRolesAsync(_user).Result.ToList();
                foreach (var roleName in roleNames)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleName));
                }

                //SignKey
                var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSettings:SignKey")));

                //產生 token
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _configuration.GetValue<string>("JwtSettings:Issuer"),
                    Expires = DateTime.UtcNow.AddHours(8).AddMinutes(30),  //8小時30分 Expires 
                    Subject = new ClaimsIdentity(claims),
                    SigningCredentials = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return new
                {
                    Status = true,
                    Message = "登入成功",
                    Token = tokenString,
                    CName = _user.CName
                };

            }
        }
        public ResponseViewModel CreateRole(CreateRoleParameter createRoleParameter)
        {
            ResponseViewModel _responseViewModel = new ResponseViewModel();
            TaoyuanBIMRole _role = _roleManager.FindByNameAsync(createRoleParameter.RoleName).Result;
            if (_role != null)
            {
                _responseViewModel.Status = false;
                _responseViewModel.Message = "此角色已存在";
                return _responseViewModel;
            }
            _role = new TaoyuanBIMRole
            {
                Name = createRoleParameter.RoleName,
                Description = createRoleParameter.RoleDescription
            };
            var result = _roleManager.CreateAsync(_role).Result;
            if (result.Succeeded)
            {
                _responseViewModel.Status = true;
                _responseViewModel.Message = "建立角色成功";
                return _responseViewModel;
            }
            else
            {
                string errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                _responseViewModel.Status = false;
                _responseViewModel.Message = errorMessage;
                return _responseViewModel;
            }
        }
        public ResponseViewModel AssignRole(AssignRoleParameter assignRoleParameter)
        {
            ResponseViewModel _responseViewModel = new ResponseViewModel();
            TaoyuanBIMUser _user = _userManager.FindByNameAsync(assignRoleParameter.UserName).Result;
            if (_user != null)
            {
                var result = _userManager.AddToRoleAsync(_user, assignRoleParameter.RoleName).Result;
                if (result.Succeeded)
                {
                    var _role = _roleManager.FindByNameAsync(assignRoleParameter.RoleName).Result;
                    _responseViewModel.Status = true;
                    _responseViewModel.Message = $"{_user.CName}指派為{_role.Description}成功";
                    return _responseViewModel;
                }
                else
                {
                    string errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                    _responseViewModel.Status = false;
                    _responseViewModel.Message = errorMessage;
                    return _responseViewModel;
                }
            }
            else
            {
                _responseViewModel.Status = false;
                _responseViewModel.Message = "此帳號不存在";
                return _responseViewModel;
            }

        }

    }
}

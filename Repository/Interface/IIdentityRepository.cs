using Microsoft.AspNetCore.Mvc;
using TaoyuanBIMAPI.Parameter;
using TaoyuanBIMAPI.ViewModel;

namespace TaoyuanBIMAPI.Repository.Interface
{
    public interface IIdentityRepository
    {
        ResponseViewModel Register(RegisterParameter registerParameter);
        object Login(LoginPrarmeter loginPrarmeter);
        ResponseViewModel CreateRole(CreateRoleParameter createRoleParameter);
        ResponseViewModel AssignRole(AssignRoleParameter assignRoleParameter);
    }
}

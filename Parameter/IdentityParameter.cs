namespace TaoyuanBIMAPI.Parameter
{
    public class RegisterParameter
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string CName { get; set; }
        public string? Department { get; set; }
        public string? Title { get; set; }
        public string? Phone { get; set; }

    }
    public class  LoginPrarmeter
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class CreateRoleParameter
    {
        public string RoleName { get; set; }
        public string? RoleDescription { get; set; }
    }
    public class AssignRoleParameter
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}

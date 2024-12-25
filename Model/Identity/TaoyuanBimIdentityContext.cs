using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TaoyuanBIMAPI.Model.Identity
{
    public class TaoyuanBimIdentityContext : IdentityDbContext<TaoyuanBIMUser, TaoyuanBIMRole, string>
    {
        public TaoyuanBimIdentityContext(DbContextOptions<TaoyuanBimIdentityContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

public class TaoyuanBIMUser : IdentityUser
{
    [Required]
    [MaxLength(30)]
    public string CName { get; set; }

    [MaxLength(50)]
    public string? Department { get; set; }

    [MaxLength(30)]
    public string? Title { get; set; }

    [MaxLength(30)]
    public string? Phone { get; set; }

    public DateTime? ExpirationDate { get; set; }

    [BindNever]
    public bool Availablility { get; set; }
}
public class TaoyuanBIMRole : IdentityRole
{
    
    public string? Description { get; set; }

}

using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;

namespace Persistence.Extensions
{
    public static class IdentityExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services) {
            IdentityBuilder builder = services.AddIdentityCore<User>(opts => {
                opts.Lockout.MaxFailedAccessAttempts = 10;
                opts.User.RequireUniqueEmail = false;
                opts.Password.RequiredLength = 7;
                opts.Password.RequiredUniqueChars = 0;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireLowercase = false;
            });
            // .AddUserValidator<UniqueEmail<User>>();

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DefaultDataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            builder.AddDefaultTokenProviders();
        }
    }
}
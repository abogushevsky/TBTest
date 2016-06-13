using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using TaskManager.Common.Entities;
using TaskManager.Common.Interfaces;
using TaskManager.Web.Models;

namespace TaskManager.Web
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly IUsersService usersService;

        public ApplicationUserManager(IUserStore<ApplicationUser> store, IUsersService usersService)
            : base(store)
        {
            Contract.Requires(store != null);
            Contract.Requires(usersService != null);

            this.usersService = usersService;
        }

        //#region Overrides of UserManager<ApplicationUser,string>

        ///// <summary>Create a user with the given password</summary>
        ///// <param name="user"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //public override async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        //{
        //    IdentityResult result = await base.CreateAsync(user, password);

        //    ApplicationUser createdUser = await FindByNameAsync(user.UserName);
        //    if (createdUser != null)
        //    {
        //        try
        //        {
        //            await this.usersService.AddUserAsync(new UserInfo()
        //            {
        //                Id = createdUser.Id,
        //                FirstName = user.FirstName,
        //                LastName = user.LastName
        //            });
        //        }
        //        catch
        //        {
        //            await DeleteAsync(createdUser);
        //        }
        //    }

        //    return result;
        //}

        //#endregion

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()), SimpleInjectorWebApiInitializer.Resolve<IUsersService>());
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            IDataProtectionProvider dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}

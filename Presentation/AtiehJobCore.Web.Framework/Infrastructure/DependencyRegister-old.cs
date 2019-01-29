//using AtiehJobCore.Common.Configuration;
//using AtiehJobCore.Common.Contracts;
//using AtiehJobCore.Common.Infrastructure;
//using AtiehJobCore.Common.Web;
//using AtiehJobCore.Data.DbContext;
//using AtiehJobCore.Domain.Entities.Identity;
//using AtiehJobCore.Services.Identity;
//using AtiehJobCore.Services.Identity.Interfaces;
//using AtiehJobCore.Services.Identity.Logger;
//using Autofac;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

//namespace AtiehJobCore.Web.Framework.Infrastructure
//{
//    public class DependencyRegister : IDependencyRegistrar
//    {
//        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, CommonConfig config)
//        {
//            builder.RegisterType<AtiehJobCoreDbContext>().As<IUnitOfWork>().InstancePerLifetimeScope();

//            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

//            builder.RegisterType<Normalizer>().As<ILookupNormalizer>().InstancePerLifetimeScope();
//            builder.RegisterType<Normalizer>().As<UpperInvariantLookupNormalizer>().InstancePerLifetimeScope();

//            builder.RegisterType<Services.Identity.SecurityStampValidator>().As<ISecurityStampValidator>().InstancePerLifetimeScope();
//            builder.RegisterType<Services.Identity.SecurityStampValidator>().As<SecurityStampValidator<User>>().InstancePerLifetimeScope();

//            builder.RegisterType<PasswordValidator>().As<IPasswordValidator<User>>().InstancePerLifetimeScope();
//            builder.RegisterType<PasswordValidator>().As<PasswordValidator<User>>().InstancePerLifetimeScope();

//            builder.RegisterType<UserValidator>().As<IUserValidator<User>>().InstancePerLifetimeScope();
//            builder.RegisterType<UserValidator>().As<UserValidator<User>>().InstancePerLifetimeScope();

//            builder.RegisterType<ClaimsPrincipalFactory>().As<IUserClaimsPrincipalFactory<User>>().InstancePerLifetimeScope();
//            builder.RegisterType<ClaimsPrincipalFactory>().As<UserClaimsPrincipalFactory<User, Role>>().InstancePerLifetimeScope();

//            builder.RegisterType<IdentityErrorDescriber>().As<IdentityErrorDescriber>().InstancePerLifetimeScope();

//            builder.RegisterType<Services.Identity.UserStore>().As<IUserStore>().InstancePerLifetimeScope();
//            builder.RegisterType<Services.Identity.UserStore>().As<UserStore<User, Role, AtiehJobCoreDbContext, int,
//                UserClaim, UserRole, UserLogin, UserToken, RoleClaim>>().InstancePerLifetimeScope();

//            builder.RegisterType<UserManager>().As<IUserManager>().InstancePerLifetimeScope();
//            builder.RegisterType<UserManager>().As<UserManager<User>>().InstancePerLifetimeScope();

//            //builder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>().InstancePerLifetimeScope();


//            builder.RegisterType<RoleManager>().As<IRoleManager>().InstancePerLifetimeScope();
//            builder.RegisterType<RoleManager>().As<RoleManager<Role>>().InstancePerLifetimeScope();

//            builder.RegisterType<SignInManager>().As<ISignInManager>().InstancePerLifetimeScope();
//            builder.RegisterType<SignInManager>().As<SignInManager<User>>().InstancePerLifetimeScope();

//            builder.RegisterType<RoleStore>().As<IRoleStore>().InstancePerLifetimeScope();
//            builder.RegisterType<RoleStore>().As<RoleStore<Role, AtiehJobCoreDbContext, int,
//                UserRole, RoleClaim>>().InstancePerLifetimeScope();

//            builder.RegisterType<MessageSender>().As<IEmailSender>().InstancePerLifetimeScope();
//            builder.RegisterType<MessageSender>().As<ISmsSender>().InstancePerLifetimeScope();

//            builder.RegisterType<IdentityDbInitializer>().As<IIdentityDbInitializer>().InstancePerLifetimeScope();

//            builder.RegisterType<UsedPasswordsService>().As<IUsedPasswordsService>().InstancePerLifetimeScope();

//            builder.RegisterType<SiteStateService>().As<ISiteStateService>().InstancePerLifetimeScope();

//            builder.RegisterType<UsersPhotoService>().As<IUsersPhotoService>().InstancePerLifetimeScope();

//            builder.RegisterType<SecurityTrimmingService>().As<ISecurityTrimmingService>().InstancePerLifetimeScope();

//            builder.RegisterType<LogItemsService>().As<ILogItemsService>().InstancePerLifetimeScope();

//            builder.RegisterType<RazorViewRenderer>().As<IRazorViewRenderer>().InstancePerLifetimeScope();
//            builder.RegisterType<CustomFileProvider>().As<ICustomFileProvider>().InstancePerLifetimeScope();

//            //builder.RegisterType<NoBrowserCacheAntiforgery>().As<IAntiforgery>().InstancePerLifetimeScope();
//            //builder.RegisterType<NoBrowserCacheHtmlGenerator>().As<IHtmlGenerator>().InstancePerLifetimeScope();

//        }

//        public int Order => 0;
//    }
//}

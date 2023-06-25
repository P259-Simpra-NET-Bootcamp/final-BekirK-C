using Autofac;
using Base.Utilities.Security.JWT;
using Business.Abstract;
using Business.Concrete;
using Business.ValidationRules;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Models.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Business.DependencyResolver.Autofac;

public class DependencyResolverModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
        builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

        builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
        builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

        builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
        builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

        builder.RegisterType<CartManager>().As<ICartService>().SingleInstance();
        builder.RegisterType<EfCartDal>().As<ICartDal>().SingleInstance();

        builder.RegisterType<EfOrderDal>().As<IOrderDal>().SingleInstance();
        builder.RegisterType<CartOrderManager>().As<ICartOrderService>().SingleInstance();

        builder.RegisterType<CouponManager>().As<ICouponService>().SingleInstance();
        builder.RegisterType<EfCouponDal>().As<ICouponDal>().SingleInstance();

        builder.RegisterType<AuthManager>().As<IAuthService>();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>();

        builder.RegisterType<WalletManager>().As<IWalletService>().SingleInstance();

        builder.RegisterType<CreditCardPolicyManager>().As<ICreditCardPolicyService>().SingleInstance();

        builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();

        builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

        builder.RegisterType<CategoryValidator>().As<IValidator<Category>>();
        builder.RegisterType<UserRegisterValidator>().As<IValidator<UserRegisterRequest>>();
        builder.RegisterType<CouponValidator>().As<IValidator<CouponRequest>>();
        builder.RegisterType<ProductValidator>().As<IValidator<Product>>();
        builder.RegisterType<CreditCardInfoValidator>().As<IValidator<CreditCardInfoRequest>>();
    }
}
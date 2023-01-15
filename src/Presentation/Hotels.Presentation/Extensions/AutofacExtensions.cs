using Autofac;
using Autofac.Extensions.DependencyInjection;
using HotelsApp.Data.Abstractions;
using HotelsApp.Infrastructure.Models;
using HotelsApp.Infrastructure.Repositories;

namespace HotelsApp.Presentation.Extensions
{
    public static class AutofacExtensions
    {
        #region Public methods
        public static void RegisterApplicationServices(WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(builder =>
            {
                builder.RegisterType<HotelRepository>()
                .As<IHotelRepository>()
                .InstancePerLifetimeScope();

                builder.RegisterType<LocationHotelRepository>()
                .As<ILocationHotelRepository>()
                .InstancePerLifetimeScope();

                builder.RegisterType<Hotels>()
                .SingleInstance();
            }));
        }
        #endregion
    }
}

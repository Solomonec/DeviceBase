using DeviceBase.BLL.Implement;
using DeviceBase.BLL.Interfaces;
using DeviceBase.Code.Implement;
using DeviceBase.Code.Interfaces;
using DeviceBase.Models;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DeviceBase.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(DeviceBase.App_Start.NinjectWebCommon), "Stop")]

namespace DeviceBase.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Extensions.Factory;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDeviceRepository<ItDevice>>().To<ItDeviceRepository>().WithConstructorArgument("context", new DeviceContext());
            kernel.Bind<IDeviceRepository<AsppDevice>>().To<AsppDeviceRepository>().WithConstructorArgument("context", new DeviceContext());
            kernel.Bind<IDeviceRepository<PaDevice>>().To<PaDeviceRepository>().WithConstructorArgument("context", new DeviceContext());
            kernel.Bind<ILogRepository<ItDeviceLog>>().To<ItLogRepository>().WithConstructorArgument("context", new DeviceContext());
            kernel.Bind<ILogRepository<AsppDeviceLog>>().To<AsppLogRepository>().WithConstructorArgument("context", new DeviceContext());
            kernel.Bind<ILogRepository<PaDeviceLog>>().To<PaLogRepository>().WithConstructorArgument("context", new DeviceContext());
            kernel.Bind<ILocationRepository>().To<LocationRepository>().WithConstructorArgument("context", new DeviceContext());
            kernel.Bind<IDeviceClassRepository>().To<DeviceClassRepository>().WithConstructorArgument("context", new DeviceContext());
            kernel.Bind<IDeviceTypeRepository>().To<DeviceTypeRepository>().WithConstructorArgument("context", new DeviceContext());
            kernel.Bind<IOwnerRepository>().To<OwnerRepository>().WithConstructorArgument("context", new DeviceContext());
            kernel.Bind<IUserRepository>().To<UserRepository>().WithConstructorArgument("context", new UserContext());
            kernel.Bind<IDataFilter>().To<DataFilter>().WithConstructorArgument("context", new DeviceContext()); 
            kernel.Bind<IDataExport>().To<DataExport>();
            kernel.Bind<IStatistics>().To<Statistics>();
           
            kernel.Bind<IFactoryRepository>().ToFactory();
        }        
    }
}

using System;
using System.Web.Mvc;
using ComClassLibrary.Repo;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace WebAppTest
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
    

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
          var uc=new UnityContainer();
            RegisterTypes(uc);
            return uc;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();

            //registering Unity for web API
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            //registering Unity for MVC
       
            container.RegisterType(typeof(IRepository<,>), typeof(Repo<,>), new HierarchicalLifetimeManager(), new InjectionConstructor(new GenericParameter("TContext")));
         
        }
    }
}
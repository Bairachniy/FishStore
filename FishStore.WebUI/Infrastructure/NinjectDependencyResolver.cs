using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using FishStore.Domain.Abstract;
using FishStore.Domain.Entities;
using FishStore.Domain.Concrete;
using FishStore.WebUI.Infrastructure.Abstract;
using FishStore.WebUI.Infrastructure.Concrete;

namespace FishStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //Mock<IFishRepository> mock = new Mock<IFishRepository>();
            //mock.Setup(m => m.Fishes).Returns(new List<Fish>
            //{
            //    new Fish { Name = "Тарань", Price = 295 },
            //    new Fish { Name = "Вобла", Price=120 },
            //    new Fish { Name = "Сельдь", Price=65 }
            //});
            //kernel.Bind<IFishRepository>().ToConstant(mock.Object);
            kernel.Bind<IFishRepository>().To<EFFishRepository>();
           

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}
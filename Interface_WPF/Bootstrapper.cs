using Caliburn.Micro;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro.Autofac;
using Interface_WPF.Login.ViewModels;
using Interface_WPF.Content.ViewModels;
using Interface_WPF.Content.Repositories;
using System.Collections.Concurrent;

namespace Interface_WPF
{
    public class Bootstrapper: AutofacBootstrapper<ShellViewModel>
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<ShellViewModel>().SingleInstance();


            builder.RegisterType<LogingConductorViewModel>().SingleInstance();
            builder.RegisterType<LoginCredentialsViewModel>().SingleInstance();
            builder.RegisterType<Login2FAViewModel>().SingleInstance();

            builder.RegisterType<ContentConductorViewModel>().SingleInstance();
            builder.RegisterType<HomeViewModel>().SingleInstance();
            builder.RegisterType<SettingViewModel>().SingleInstance();

            builder.RegisterType<HeaderViewModel>().SingleInstance();
            builder.RegisterType<OrdersViewModel>().SingleInstance();
            builder.RegisterType<ContentHeaderViewModel>().SingleInstance();

            builder.RegisterType<BooksRepository>().SingleInstance();
            builder.RegisterType<OrdersRepository>().SingleInstance();

            builder.RegisterType<BooksRepository>()
                .As<IBooksRepository>()
                .SingleInstance();

           builder.RegisterType<OrdersRepository>()
                .As<IOrdersRepository>()
                .SingleInstance();

        }
        protected override void ConfigureBootstrapper()
        {
            base.ConfigureBootstrapper();
        }
        protected  override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        
    }
}

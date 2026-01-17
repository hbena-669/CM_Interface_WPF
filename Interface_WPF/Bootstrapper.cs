using Autofac;
using Caliburn.Micro;
using Caliburn.Micro.Autofac;
using Interface_WPF.Content.Repositories;
using Interface_WPF.Content.ViewModels;
using Interface_WPF.Interfaces;
using Interface_WPF.Login.ViewModels;
using Interface_WPF.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            builder.RegisterType<HomeComptaViewModel>().SingleInstance();
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

            builder.RegisterType<AuthApi>()
                .As<IAuthApi>()
                .SingleInstance();

            builder.RegisterType<ContextService>()
                .As<IContextService>()
                .SingleInstance();

            builder.Register(c =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                return new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://localhost:7237/")
                };
            })
            .As<HttpClient>()
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

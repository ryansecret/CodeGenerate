using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using RCodeGenerator.ViewModel;
using Repositroy;
using Service;

namespace RCodeGenerator
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static IContainer Container;
        public App()
        {   
            Autofac.ContainerBuilder builder=new ContainerBuilder();
            builder.RegisterType<MySqlDbInfoRepository>().AsImplementedInterfaces();
            builder.RegisterType<DbService>();
            builder.RegisterType<MainVm>();
            Container= builder.Build();
        }
    }
}

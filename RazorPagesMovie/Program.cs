using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RazorPagesMovie
{
    /// <summary>
    /// 托管 ASP.NET Core 应用
    /// 创建一个 web 应用程序宿主
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            //初始化web主机
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }

    /// <summary>
    /// WebHost.CreateDefaultBuilder 是在 2.0 中新增的
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    //public static IWebHostBuilder CreateDefaultBuilder(string[] args)
    //{
    //    var builder = new WebHostBuilder()
    //        .UseKestrel()//注册 Kestrel 中间件，指定 WebHost 要使用的 Server（HTTP服务器）。
    //        .UseContentRoot(Directory.GetCurrentDirectory())//设置 Content 根目录，将当前项目的根目录作为 ContentRoot 的目录。
    //        .ConfigureAppConfiguration((hostingContext, config) =>
    //        {
    //读取 appsettinggs.json 配置文件，开发环境下的 UserSecrets 以及环境变量和命令行参数。
    //            var env = hostingContext.HostingEnvironment;

    //            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    //                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

    //            if (env.IsDevelopment())
    //            {
    //                var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
    //                if (appAssembly != null)
    //                {
    //                    config.AddUserSecrets(appAssembly, optional: true);
    //                }
    //            }

    //            config.AddEnvironmentVariables();

    //            if (args != null)
    //            {
    //                config.AddCommandLine(args);
    //            }
    //        })
    //        .ConfigureLogging((hostingContext, logging) =>
    //        {
    //读取配置文件中的 Logging 节点，对日志系统进行配置
    //            logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    //            logging.AddConsole();
    //            logging.AddDebug();
    //        })
    //        .UseIISIntegration()//添加 IISIntegration 中间件
    //        .UseDefaultServiceProvider((context, options) =>
    //        {
    //设置开发环境下， ServiceProvider 的 ValidateScopes 为 true，避免直接在 Configure 方法中获取 Scope 实例
    //            options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
    //        });
    //    return builder;
    //}


}

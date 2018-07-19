using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using RazorPagesMovie.Models;

namespace RazorPagesMovie
{
    /// <summary>
    /// 配置服务和请求管道
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 通过依赖注入将服务注册到容器
        /// 服务是应用中用于通用调用的组件。服务通过依赖注入获取并使用
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;//禁用将客户端证书转发到应用
            });

            //向依赖关系注入容器注册数据库上下文
            services.AddDbContext<MovieContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("MovieContext")));


            services.AddMvc();//默认将MVC的服务添加到容器中
        }

        /// <summary>
        /// 定义请求管道中间件
        /// ASP.NET Core 中间件为一个 HttpContext 执行异步逻辑，然后按顺序调用下一个中间件或者直接终止请求。
        /// 一般来说你要使用一个中间件，只需要在 Configure 方法里调用 IApplicationBuilder 上一个对应的 UseXYZ 扩展方法。
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //接收到一个请求时，请求会交给中间件构成的中间件管道进行处理，管道就是多个中间件构成，
            //请求从一个中间件的一端进入，从中间件的另一端出来，每个中间件都可以对HttpContext请求开始和结束进行处理


            //Run()一般在管道尾部被调用；Use可以调用next方法；Map用于分支管道(当前的实现已支持基于请求路径或使用谓词来进入分支)  

            //在开发环境下，显示错误详细信息
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //否则，导向错误页面
                app.UseExceptionHandler("/Error");
            }


            //使用默认起始页(默认根目录是wwwroot)
            //DefaultFilesOptions options = new DefaultFilesOptions();
            //options.DefaultFileNames.Clear();
            //options.DefaultFileNames.Add("index.htm");
            //app.UseDefaultFiles();



            //允许访问wwwroot文件夹下的静态文件
            app.UseStaticFiles();

                 
            app.UseMvc();
        }
    }
}

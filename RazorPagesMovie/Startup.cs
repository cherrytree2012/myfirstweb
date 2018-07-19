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
    /// ���÷��������ܵ�
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// ͨ������ע�뽫����ע�ᵽ����
        /// ������Ӧ��������ͨ�õ��õ����������ͨ������ע���ȡ��ʹ��
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;//���ý��ͻ���֤��ת����Ӧ��
            });

            //��������ϵע������ע�����ݿ�������
            services.AddDbContext<MovieContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("MovieContext")));


            services.AddMvc();//Ĭ�Ͻ�MVC�ķ�����ӵ�������
        }

        /// <summary>
        /// ��������ܵ��м��
        /// ASP.NET Core �м��Ϊһ�� HttpContext ִ���첽�߼���Ȼ��˳�������һ���м������ֱ����ֹ����
        /// һ����˵��Ҫʹ��һ���м����ֻ��Ҫ�� Configure ��������� IApplicationBuilder ��һ����Ӧ�� UseXYZ ��չ������
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //���յ�һ������ʱ������ύ���м�����ɵ��м���ܵ����д����ܵ����Ƕ���м�����ɣ�
            //�����һ���м����һ�˽��룬���м������һ�˳�����ÿ���м�������Զ�HttpContext����ʼ�ͽ������д���


            //Run()һ���ڹܵ�β�������ã�Use���Ե���next������Map���ڷ�֧�ܵ�(��ǰ��ʵ����֧�ֻ�������·����ʹ��ν���������֧)  

            //�ڿ��������£���ʾ������ϸ��Ϣ
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //���򣬵������ҳ��
                app.UseExceptionHandler("/Error");
            }


            //ʹ��Ĭ����ʼҳ(Ĭ�ϸ�Ŀ¼��wwwroot)
            //DefaultFilesOptions options = new DefaultFilesOptions();
            //options.DefaultFileNames.Clear();
            //options.DefaultFileNames.Add("index.htm");
            //app.UseDefaultFiles();



            //�������wwwroot�ļ����µľ�̬�ļ�
            app.UseStaticFiles();

                 
            app.UseMvc();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TokenTalk.Models;

namespace TokenTalk.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // TalkApp ���� ������(���Ӽ�) ���� ���� �ڵ常 ���� ��Ƽ� ���� 
            AddDependencyInjectionContainerForTalkApp(services);

            #region CORS
            //[CORS][1] CORS ��� ���
            //[CORS][1][1] �⺻: ��� ���
            services.AddCors(options =>
            {
                //[A] [EnableCors] Ư������ ���� ���� 
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
                //[B] [EnableCors("AllowAnyOrigin")] ���·� ���� ����
                options.AddPolicy("AllowAnyOrigin", builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            #endregion
        }

        /// <summary>
        /// TalkApp ���� ������(���Ӽ�) ���� ���� �ڵ常 ���� ��Ƽ� ���� 
        /// </summary>
        private void AddDependencyInjectionContainerForTalkApp(IServiceCollection services)
        {
            // TalkAppDbContext.cs Inject: New DbContext Add
            services.AddEntityFrameworkSqlServer().AddDbContext<TalkAppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // ITalkRepository.cs Inject: DI Container�� ����(�������丮) ��� 
            services.AddTransient<ITalkRepository, TalkRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(); //�ӽ÷� ����

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

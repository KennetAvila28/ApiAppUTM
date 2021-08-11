using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Repositories;
using AppUTM.Data;
using AppUTM.Data.Repositories;
using AppUTM.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace AppUTM.Api
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
            //services.AddControllers();
            services.AddCors(setupAction =>
            {
                setupAction.AddPolicy("default", p =>
                {
                    p.WithOrigins("http://localhost:44301")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddMicrosoftIdentityWebApiAuthentication(Configuration);
            //services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IEmpresaServices, EmpresaService>();
            services.AddTransient<ICuponGenericoServices, CuponGenericoService>();
            services.AddTransient<ICuponImagenServices, CuponImagenService>();
            services.AddTransient<IHistorialCuponesServices, HistorialCuponesService>();
            services.AddTransient<IAlmacenarImagen, AlmacenarImagen>();           
            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiAppUTM", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiAppUTM v1"));
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
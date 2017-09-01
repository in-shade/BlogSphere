using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BlogSphere.Data;
using Microsoft.EntityFrameworkCore;
using BlogSphere.Controllers;
using BlogSphere.Repositories;
using BlogSphere.Models;

namespace BlogSphere
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<BlogSphereContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SQLServer")));

            services.AddTransient<BlogSphereInitialize>();
            
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IPostRepository, PostRepository>();
            services.AddSingleton<ITagRepository, TagRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, BlogSphereInitialize seeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            seeder.Initialize();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.User, ViewModels.UserViewModel>().ReverseMap();
                cfg.CreateMap<Models.Post, ViewModels.PostViewModel>()
                     .ForMember(dest => dest.Nickname,
                                opts => opts.MapFrom(src => src.User.Nickname))
                     .ForMember(dest => dest.Email,
                                opts => opts.MapFrom(src => src.User.Email))
                     .ReverseMap();
                cfg.CreateMap<Models.Post, ViewModels.PostCreateModel>().ReverseMap();
                cfg.CreateMap<Models.Tag, ViewModels.TagViewModel>().ReverseMap();
            });

            app.UseMvc();
        }
    }
}

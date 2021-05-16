using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ragther.business.Abstract;
using ragther.business.Concrete.Todo;
using ragther.business.Concrete.User;
using ragther.data.Abstract;
using ragther.data.Concrete.EFCore;
using ragther.data.Concrete.EFCore.TestData;
using ragther.data.Mapping;
using ragther.core.DataAccess;
using ragther.business.Concrete.ProfileDetail;
using ragther.business.Concrete.Comment;
using ragther.business.Concrete.Friendship;
using ragther.business.Concrete.Notice;
using ragther.business.Concrete.Tag;
using ragther.business.Concrete.TodoTag;
using ragther.business.Concrete.WorkWith;
using ragther.service.Hubs;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using ragther.business.Concrete.ProfileUpdate;
using ragther.business.Concrete.Like;
using ragther.business.Concrete.MailUpdate;
using ragther.business.Concrete.Remind;
using ragther.business.Concrete.TagsOfInterest;

namespace ragther.service
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            
            services.Configure<FormOptions>(o=>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            

            services.AddCors(options => 
            { 
                options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });

            services.AddSignalR();

            // Injection for Repositories -- START
            // Entitiy Framework
            services.AddScoped<ICommentRepository,EFCoreCommentRepository>();
            services.AddScoped<IFriendshipRepository,EFCoreFriendshipRepository>();
            services.AddScoped<IFriendshipConditionRepository,EFCoreFriendshipConditionRepository>();
            services.AddScoped<ILikeRepository,EFCoreLikeRepository>();
            services.AddScoped<IMailUpdateRepository,EFCoreMailUpdateRepository>();
            services.AddScoped<INoticeRepository,EFCoreNoticeRepository>();
            services.AddScoped<INoticeTypeRepository,EFCoreNoticeTypeRepository>();
            services.AddScoped<IProfileDetailRepository,EFCoreProfileDetailRepository>();
            services.AddScoped<IRemindRepository,EFCoreRemindRepository>();
            services.AddScoped<ITagRepository,EFCoreTagRepository>();
            services.AddScoped<ITagsOfInterestRepository,EFCoreTagsOfInterestRepository>();
            services.AddScoped<ITodoConditionRepository,EFCoreTodoConditionRepository>();
            services.AddScoped<ITodoRepository,EFCoreTodoRepository>();
            services.AddScoped<ITodoTagRepository,EFCoreTodoTagRepository>();
            services.AddScoped<ITokenConditionRepository,EFCoreTokenConditionRepository>();
            services.AddScoped<IUserRepository,EFCoreUserRepository>();
            services.AddScoped<IWorkWithRepository,EFCoreWorkWithRepository>();
            // Injection for Repositories -- END

            // Injection for Services -- START
            services.AddScoped<IUserService,UserManager>();
            services.AddScoped<ITodoService,TodoManager>();
            services.AddScoped<IProfileDetailService,ProfileDetailManager>();
            services.AddScoped<ICommentService,CommentManager>();
            services.AddScoped<IFriendshipService,FriendshipManager>();
            services.AddScoped<INoticeService,NoticeManager>();
            services.AddScoped<IWorkWithService,WorkWithManager>();
            services.AddScoped<ITagService,TagManager>();
            services.AddScoped<ITodoTagService,TodoTagManager>();
            services.AddScoped<IProfileUpdateService,ProfileUpdateManager>();
            services.AddScoped<ILikeService,LikeManager>();
            services.AddScoped<IMailUpdateService,MailUpdateManager>();
            services.AddScoped<IRemindService,RemindManager>();
            services.AddScoped<ITagsOfInterestService,TagsOfInterestManager>();

            // Injection for Repositories -- END

            services.AddAutoMapper(cfg => cfg.AddProfile<AppMappingProfile>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // SeedDatabase.Seed();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            
            app.UseStaticFiles( new StaticFileOptions{
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using website.bll.interfaces;
using website.bll.repositorys;
using website.dal.context;
using website.dal.NewFolder;
using website.pl.mapping;

namespace website.pl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<websitecontext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default")));
            builder.Services.AddScoped<departmentrepo>();
            builder.Services.AddScoped<Employeerepo>();
            builder.Services.AddScoped<Iunitofwork, unitofwork>();
            builder.Services.AddAutoMapper(m =>
            {
                m.AddProfile(new employeemap());
                m.AddProfile(new accountmapping()); 
            });
            builder.Services.AddIdentity<appuser,IdentityRole>()
                .AddEntityFrameworkStores<websitecontext>()
                .AddDefaultTokenProviders();
                

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Run();
        }
    }
}

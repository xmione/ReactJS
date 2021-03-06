using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactJS.Redux.Repositories;
#if CF
    using EF = ReactJS.Redux.CodeFirst;
    using EFModels = ReactJS.Redux.CodeFirst.Models;
#else
    using EF = ReactJS.Redux.DatabaseFirst;
    using EFModels = ReactJS.Redux.DatabaseFirst.Models;
#endif
using System;

namespace ReactJS.Redux.Web.API
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
            try 
            {
                services.AddCors(c =>
                {
                    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
                });
                //var connString = Configuration.GetConnectionString("RRCConnectionString");
                //var connString = "Data Source=(local);Initial Catalog=RRC;Integrated Security=True;";
                // Add EF services to the services container.
                //services.AddDbContext<RRCContext>(options =>options.UseSqlServer(connString, null)); //==> oftentimes this one line is missing
                //var conn = new SqlConnection(connString);

                //if (conn.State == System.Data.ConnectionState.Closed)
                //{
                //    conn.Open();
                //}
                services.AddDbContext<EF.RRCContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:RRCConnectionString"]));
                services.AddScoped<IDataRepository<EFModels.Person>, SQLPersonRepository>();
                services.AddControllers();
                
                
            }
            catch (Exception ex) 
            {
                var exceptionTypeName = ex.GetType().Name;
                throw new Exception("Error opening the connection [" + exceptionTypeName + "]", ex.InnerException);
            }
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

            //app.UseCors(options => options.WithOrigins("https://localhost:44337"));
            app.UseCors(options => options.AllowAnyOrigin());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}

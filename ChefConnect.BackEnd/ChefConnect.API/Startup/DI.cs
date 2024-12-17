using ChefConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChefConnect.API.Startup
{
    public static class DI
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Controllers

            // Đăng ký Controllers
            services.AddControllers();

            #endregion

            #region Swagger

            // Đăng ký Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            #endregion

            #region DBContext

            // Cấu hình DbContext
            services.AddDbContext<ChefConnectDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            #endregion

            #region CORS

            // Cấu hình CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            #endregion

            #region Services

            // Đăng ký các service khác nếu có
            // services.AddScoped<IService, ServiceImplementation>();

            #endregion
        }
    }
}

using ChefConnect.Application.Interfaces;
using ChefConnect.Application.Services;
using ChefConnect.Domain.Entities;
using ChefConnect.Infrastructure;
using ChefConnect.Infrastructure.Interfaces;
using ChefConnect.Infrastructure.Repositories;
using ChefConnect.Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ChefConnect.API.Startup
{
    public static class DI
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Setup Authentication JWT
            // Setup JWT Authentication
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, // Enable issuer validation
                        ValidateAudience = true, // Enable audience validation
                        ValidateLifetime = true, // Enable token lifetime validation
                        ValidateIssuerSigningKey = true, // Ensure the signing key is valid
                        ValidIssuer = configuration["JWT:Issuer"], // Issuer from config
                        ValidAudience = configuration["JWT:Audience"], // Audience from config
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])), // Secret key
                        ClockSkew = TimeSpan.Zero // Optional: remove clock skew for precise expiration
                    };
                });
            #endregion

            #region Setup Authorization
            services.AddAuthorization(options =>
            {
                // Admin-only policy
                options.AddPolicy(
                    "AdminOnly",
                    policyBuilder => policyBuilder.RequireAssertion(
                        context => context.User.HasClaim(
                            claim => claim.Type == "Role" && claim.Value == "Admin"
                        )
                    )
                );

                // Chef-only policy
                options.AddPolicy(
                    "ChefOnly",
                    policyBuilder => policyBuilder.RequireAssertion(
                        context => context.User.HasClaim(
                            claim => claim.Type == "Role" && claim.Value == "Chef"
                        )
                    )
                );

                // Customer-only policy
                options.AddPolicy(
                    "CustomerOnly",
                    policyBuilder => policyBuilder.RequireAssertion(
                        context => context.User.HasClaim(
                            claim => claim.Type == "Role" && claim.Value == "Customer"
                        )
                    )
                );

                // Admin or Chef policy
                options.AddPolicy(
                    "AdminOrChef",
                    policyBuilder => policyBuilder.RequireAssertion(
                        context => context.User.HasClaim(
                            claim => claim.Type == "Role" &&
                            (claim.Value == "Admin" || claim.Value == "Chef")
                        )
                    )
                );

                // Admin, Chef, or Customer policy
                options.AddPolicy(
                    "AdminChefOrCustomer",
                    policyBuilder => policyBuilder.RequireAssertion(
                        context => context.User.HasClaim(
                            claim => claim.Type == "Role" &&
                            (claim.Value == "Admin" || claim.Value == "Chef" || claim.Value == "Customer")
                        )
                    )
                );
            });
            #endregion

            #region Setup Swagger Bearer

            services.AddSwaggerGen(c =>
            {
                // Define the security scheme
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", jwtSecurityScheme);

                // Define the security requirement
                var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            };

                c.AddSecurityRequirement(securityRequirement);
            });

            #endregion

            #region UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

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
            services.AddLogging();
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();


            #endregion

            #region Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUserRepository, UserRepository>();


            #endregion
        }
    }
}

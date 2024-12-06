using Attendance_Student.MapperConfig;
using Attendance_Student.Models;
using Attendance_Student.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


namespace Attendance_Student
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();

            // Configure Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(op =>
            {
                op.EnableAnnotations();
                op.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Attendance Recording System API",
                    Version = "v1",
                    Description = "APIs for attendance recording system",
                    TermsOfService = new Uri("https://github.com/Attendance-Seekers"),
                    Contact = new OpenApiContact
                    {
                        Name = "Attendance Seekers"
                    }
                });

                // JWT Bearer Authentication for Swagger
                op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT Bearer token"
                });

                op.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                });
            });

            // Database Context
            builder.Services.AddDbContext<AttendanceStudentContext>(op =>
                op.UseLazyLoadingProxies().UseSqlServer(
                    builder.Configuration.GetConnectionString("AttendanceConn")
                )
            );

            // Identity Configuration
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AttendanceStudentContext>()
            .AddDefaultTokenProviders();

            // JWT Authentication
            var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"]);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Authorization Policies
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("StudentPolicy", policy =>
                    policy.RequireRole("Student"));
                options.AddPolicy("TeacherPolicy", policy =>
                    policy.RequireRole("Teacher"));
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireRole("Administrator"));
                options.AddPolicy("ParentPolicy", policy =>
                    policy.RequireRole("Parent"));
            });

            // Repositories
            builder.Services.AddScoped<GenericRepository<Class>>();
            builder.Services.AddScoped<GenericRepository<Department>>();

            builder.Services.AddScoped<GenericRepository<Subject>>();
            builder.Services.AddScoped<GenericRepository<TimeTable>>();
            // enable Cross-Origin Requests CORS
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            // inject AutoMapper Dependancy
            builder.Services.AddAutoMapper(typeof(mapperConfig));


            var app = builder.Build();

            // Seed Roles
            var scope = app.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await RoleSeeder.SeedRolesAsync(roleManager);

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Add Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }

}

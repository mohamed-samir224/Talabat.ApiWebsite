
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contracts;
using Talabat.Core.Services;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Repository.Repositories;
using Talabat.Service.Services;

namespace Talabat.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));
            builder.Services.AddAutoMapper(typeof(MappingProf));

            #region StoreContext
            builder.Services.AddScoped<StoreContext>();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
            #endregion

            #region IdentityStoreContext
            builder.Services.AddTransient<IdentityStoreContext>();
            builder.Services.AddDbContext<IdentityStoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));

            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true; // Special chars
                options.Password.RequiredLength = 8;
            })
                .AddEntityFrameworkStores<IdentityStoreContext>();

            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                    };
                }
                ); 
            
			//builder.Services.AddScoped<>();
			#endregion

			#region Redis And Baskett

			builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(connection);


            });
            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository)); 
            #endregion

            #region Handling_ApiResponse
            builder.Services.Configure<ApiBehaviorOptions>(Options =>


                Options.InvalidModelStateResponseFactory = ((actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                         .SelectMany(P => P.Value.Errors)
                                                         .Select(M => M.ErrorMessage)
                                                         .ToList();


                    var ErrorVal = new ApiValidationErrorResponse(errors);

                    return new BadRequestObjectResult(ErrorVal);

                })


                );
			#endregion

           
			var app = builder.Build();

            #region Ask CLR Explicitly Creating Obj From Services
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;
            #endregion

            #region Requierd System & Migrate Async (Store & Identity)
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var _dbcontext = services.GetRequiredService<StoreContext>();
                await _dbcontext.Database.MigrateAsync();

                await StoreContextSeed.SeedAsync(_dbcontext);

                var _IdentityDbContext = services.GetRequiredService<IdentityStoreContext>();
                await _IdentityDbContext.Database.MigrateAsync();

                var UserManager = services.GetRequiredService<UserManager<AppUser>>();
				await IdentityContextSeed.SeedUser(UserManager);



			}
			catch (Exception ex)
            {
                var log = LoggerFactory.CreateLogger<Program>();
                //log.LogCritical(ex, "An Error Occurd In Applying Migration");
                log.LogError(ex, "an error Occured in Migrate or relative file");
            }

            #endregion

            // Configure the HTTP request pip eline.
            #region MiddleWares
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // كود محاوله لحل المشكلع بتاعه التوكن ولكن الحمد لله اتحلت بتبيل الاتين url داخل الAppSetting
    //            app.UseExceptionHandler(exceptionHandlerApp =>
				//{
				//	exceptionHandlerApp.Run(async context =>
				//	{
				//		context.Response.ContentType = "application/json";
				//		var problemDetails = new ProblemDetails
				//		{
				//			Title = "Server Error",
				//			Status = context.Response.StatusCode,
				//			Detail = "An unexpected error occurred"
				//		};
				//		await context.Response.WriteAsJsonAsync(problemDetails);
				//	});
				//});
			}

			app.UseHttpsRedirection();
            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseStaticFiles();
          
            app.UseRouting();
			app.UseAuthentication();
            app.UseAuthorization();
			app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}

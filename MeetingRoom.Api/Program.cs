using MeetingRoom.Core.Interfaces;
using MeetingRoom.Core.Models;
using MeetingRoom.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.Xml;
using Swashbuckle.AspNetCore.Swagger;
using MeetingRoom.Core.Models.Auth;
using MeetingRoom.Api.Settings;
using MeetingRoom.Api.Extensions;
using Microsoft.OpenApi.Models;
using MeetingRoom.Services.Interfaces;
using MeetingRoom.Services;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

// Add services to the container.

builder.Services.AddControllers();
var dataAssemblyName = typeof(MeetingRoomContext).Assembly.GetName().Name;
builder.Services.AddDbContext<MeetingRoomContext>(options => options.UseSqlServer(
	builder.Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("MeetingRoom.Core")));

// adds the metadata values ($id, $values)
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);


//builder.Services.AddControllers().AddJsonOptions(x =>
//   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddIdentity<UserAuth, Role>(options =>
{
	options.Password.RequiredLength = 8;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireUppercase = true;
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
	options.Lockout.MaxFailedAccessAttempts = 5;
}).AddEntityFrameworkStores<MeetingRoomContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ICompanyProfileService, CompanyProfileService>();
builder.Services.AddTransient<IRoomService, RoomService>();
builder.Services.AddTransient<IMeetingService, MeetingService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "Meetings", Version = "v1" });

	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT containing userid claim",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
	});

	var security =
		new OpenApiSecurityRequirement
		{
			{
				new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Id = "Bearer",
							Type = ReferenceType.SecurityScheme
						},
						UnresolvedReference = true
				},
				new List<string>()
			}
		};
	options.AddSecurityRequirement(security);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuth(jwtSettings);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:44338", "http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{	
    app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meetings V1");
	});
}

app.UseCors(builder =>
{
	builder
	.AllowAnyOrigin()
	.AllowAnyMethod()
	.AllowAnyHeader();
});

app.UseAuth();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

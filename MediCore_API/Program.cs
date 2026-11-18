using MediCore_API.Application.Services;
using MediCore_API.Domain.Interfaces.IRepositories;
using MediCore_API.Domain.Interfaces.Iservices;
using MediCore_API.Infrastructure.Data;
using MediCore_API.Infrastructure.Repositories;
using MediCore_Library.Models.Identities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IModelValidation, ModelValidation>();
builder.Services.AddScoped<ITimeSlotHandler, TimeSlotHandler>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IEmailSender<ApplicationUser>, EmailService>();
builder.Services.AddScoped<IAuthorizeRep, AuthorizeRepo>();
builder.Services.AddScoped<IAddressRepo, AddressRepo>();

builder.Services.AddDbContext<MediCoreContext>(options =>
				options.UseNpgsql(builder.Configuration.GetConnectionString("MediCoreContext")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
				.AddEntityFrameworkStores<MediCoreContext>()
				.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
	options.User.AllowedUserNameCharacters =
		"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 _";
});

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ModelMapper>();
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
	// Add this to handle circular references
	options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

	// Optionally, you can configure more JSON serialization settings like enum converters and ignoring nulls
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
	options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
});

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(x =>
	{
		x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(options =>
	options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
			ValidAudience = builder.Configuration["JwtSettings:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!))
		}
);

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins", policy =>
	{
		policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
	});
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
	var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
	var configuration = services.GetRequiredService<IConfiguration>();

	await SeedRolesAsync(roleManager);
	await SeedAdminUserAsync(userManager, roleManager, configuration);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapScalarApiReference();
	app.MapOpenApi();
}

app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
{
	string[] roles = { "admin", "doctor", "nurse", "patient" };
	foreach (string role in roles)
	{
		if (!await roleManager.RoleExistsAsync(role))
		{
			var newRole = new IdentityRole<Guid>
			{
				Id = Guid.NewGuid(),
				Name = role,
				NormalizedName = role.ToUpper()
			};
			await roleManager.CreateAsync(newRole);
		}
	}
}

async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration)
{
	var adminUserName = configuration["AdminUser:UserName"];
	var adminEmail = configuration["AdminUser:Email"];
	var adminPassword = configuration["AdminUser:Password"];

	var adminUser = await userManager.FindByEmailAsync(adminEmail!);
	if (adminUser == null)
	{
		adminUser = new ApplicationUser
		{
			UserName = adminUserName,
			Email = adminEmail,
			EmailConfirmed = true
		};

		var result = await userManager.CreateAsync(adminUser, adminPassword!);
		if (result.Succeeded)
		{
			await userManager.AddToRoleAsync(adminUser, "admin");
		}
		else
		{
			throw new Exception($"Admin user creation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
		}
	}
}

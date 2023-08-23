using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Reuse.Bll.DTO;
using Reuse.Bll.Repository.Implementation;
using Reuse.Bll.Repository.Interface;
using Reuse.Bll.Service.Implementation;
using Reuse.Bll.Service.Interface;
using Reuse.DAL.Data;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

//For entity framework
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));

// For Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


    // Adding Authentication
    builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})





// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});


//Identity Password
builder.Services.Configure<IdentityOptions>(options =>
{
    //Default Password Setting
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 0;
});


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .WithHeaders(HeaderNames.AccessControlRequestHeaders, HeaderNames.AccessControlAllowOrigin, HeaderNames.AccessControlAllowCredentials, HeaderNames.AccessControlAllowMethods)
            .SetIsOriginAllowed(policy => new Uri(policy).Host == "localhost").WithHeaders("Delete");

        });
});


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddScoped<IBranchServices<BranchDTO>, BranchServices>();
builder.Services.AddScoped<ISeedServices, SeedServices>();
builder.Services.AddScoped<IDistrictServices, DistrictServices>();
builder.Services.AddScoped<IMunicipalityServices, MunicipalityServices>();
builder.Services.AddScoped<IVDCServices, VDCServices>();
builder.Services.AddScoped<IFinancialStatementServices, FinancialStatementServices>(); 



builder.Services.AddScoped<IBlackListServices, BlacklistServices>();
builder.Services.AddScoped<IAuthorizationHandler, BlackListedTokenHandeler>();
builder.Services.AddAuthorization();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BlacklistedToken", policy=>policy.Requirements.Add(new BlackListedTokenRequirement()));
});








// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//Seeding data 
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seed = services.GetRequiredService<ISeedServices>();
    await seed.SeedData();

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    RoleSeeder.SeedRolesAsync(roleManager).Wait();

    //var dbContext = services.GetRequiredService<AppDbContext>();
    //dbContext.Database.Migrate();

    ////Seed Roles
    //RoleSeeder.SeedRoles(services);
}









// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseCors();

app.UseStaticFiles();

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using EmployeeVacation.Data;
using EmployeeVacation.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using User.Management.Service.Models; ////
using User.Management.Service.Services; ////

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


////////////////////////////////////////////////////////////////////////////////
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Account API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{}
        }
    });
});
/////////////////////////////////////////////////////////////////////////////////

//builder.Services.AddSwaggerGen();
//builder.Services.AddTransient<>();
/* %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();

builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% */

/*----------------------------------------------------------------------------------
//For Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentityCore<Employee>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
--------------------------------------------------------------------------------*/
//For Entity Framework
var configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//For Identity
builder.Services.AddIdentity<Employee, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//Add Config for Required Email ////
builder.Services.Configure<IdentityOptions>(
    opts => opts.SignIn.RequireConfirmedEmail = true); ////

//Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});
 
//Add Email Configs ////
var emailConfig = configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Add Cors
app.UseCors();

app.UseStaticFiles(); //I added this.
app.UseRouting(); //I added this.
app.UseAuthentication(); //I added this.

app.UseAuthorization();

app.MapControllers();

//---Beginning of: Seeding the Database
/*
var scope = app.Services.CreateScope(); //I added this.
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); //I added this.
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>(); //I added this.
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>(); //I added this.
//I added this "try-catch block"
try
{
    await context.Database.MigrateAsync();
    await DbInitializer.Initialize(context, userManager);
}
catch(Exception ex)
{
    logger.LogError(ex, "A problem occurred during migration");
}
*/
//---End of: Seeding the Database

app.Run();

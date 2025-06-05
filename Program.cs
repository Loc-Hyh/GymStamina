using System.Text;
using AutoMapper;
using CloudinaryDotNet;
using GymManagementProject.Cloudiary;
using GymManagementProject.Data;
using GymManagementProject.Entity;
using GymManagementProject.Mapper;
using GymManagementProject.Repository;
using GymManagementProject.Services.Brach;
using GymManagementProject.Services.Customer;
using GymManagementProject.Services.Equipment;
using GymManagementProject.Services.Invoice;
using GymManagementProject.Services.Schedule;
using GymManagementProject.Services.Service;
using GymManagementProject.Services.Staff;
using GymManagementProject.Services.Trainer;
using GymManagementProject.SMTP;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IServiceService, ServiceService>();

builder.Services.AddScoped<ITrainerService, TrainerService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPasswordHasher<StaffEntity>, PasswordHasher<StaffEntity>>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
//SMTP
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();
//Cloudinary
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddSingleton(x =>
{
    var config = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
    var account = new Account(config.CloudName, config.ApiKey, config.ApiSecret);
    return new Cloudinary(account);
});

//AutoMapper
//builder.Services.AddAutoMapper(typeof(Profile));
builder.Services.AddAutoMapper(typeof(MappingProfile));
// JWT config
var jwtSetting = builder.Configuration.GetSection("JwtSettings"); // Gọi config của JWT
var key = Encoding.ASCII.GetBytes(jwtSetting["Secret"]);

// Xác thực
builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSetting["Issuer"],
            ValidAudience = jwtSetting["Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization(); // Phân quyền

// Cấu hình Swagger UI để nó nhận được JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Stamina",
            Version = "v1"
        }
    );
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Nhập 'Bearer {Token}'"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
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
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());

//CORS config
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()   // Cho phép tất cả domain
                .AllowAnyMethod()   // Cho phép tất cả phương thức (GET, POST, PUT, DELETE...)
                .AllowAnyHeader();  // Cho phép tất cả headers
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
   
//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

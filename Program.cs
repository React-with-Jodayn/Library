using DotNetEnv;
using Library.Data;
using Library.Interfaces.Repositories;
using Library.Interfaces.Services;
using Library.Models;
using Library.Repositories;
using Library.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

Env.Load();// تحميل متغيرات البيئة من ملف .env


var connectionString = Environment.GetEnvironmentVariable("Database__URL");// جلب الـ ConnectionString من متغيرات البيئة

// تأكد أنه مش null (للحماية)
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string not found in environment variables.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));// إضافة DbContext باستخدام PostgreSQL

builder.Services.AddControllers();// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();// إضافة Swagger لتوثيق الـ API
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // يتجاهل الخصائص الفارغة (null) عند التحويل لـ JSON
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

    // يمنع الخطأ الناتج عن العلاقات المرجعية (مثلاً A يحتوي B و B يحتوي A)
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})//// إعداد نظام المستخدمين (Users & Roles) وربطه بالـ DB
    .AddEntityFrameworkStores<AppDbContext>();// تفعيل Identity مع EF Core(✔️ ربط الـ Identity بقاعدة البيانات باستخدام AppDbContext.)
builder.Services.AddAuthentication(options =>////تحديد طريقة المصادقة الافتراضية (JWT Bearer)//handle JWT validation
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
    // تفعيل المصادقة باستخدام JWT
    //✔️ تعريف الـ JWT Bearer Token كـ Default Scheme لكل العمليات الأمنية:
}).AddJwtBearer(options =>////إعداد تحقق الـ JWT: المفاتيح + التحقق من التوقيع + الـ Issuer/Audience
{
    options.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuer = true,// هل التوكين موثوق
        ValidateAudience = true,// هل التوكين موجه لهذا التطبيق
        ValidateIssuerSigningKey = true,// هل مفتاح التوقيع موثوق
                                        //✔️ القيم (Issuer, Audience, Key) يتم أخذها من ملف: appsettings.json
        ValidIssuer = Environment.GetEnvironmentVariable("JWT__Issuer"),
        ValidAudience = Environment.GetEnvironmentVariable("JWT__Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(
        System.Text.Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT__SigningKey")!)
    ),
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using Data;
using Data.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using productController.Services;


var builder = WebApplication.CreateBuilder(args);

// إضافة DbContext إلى خدمات التطبيق مع إعداد خيارات الاتصال بقاعدة البيانات
builder.Services.AddDbContext<AppDbContext>(optionsAction: optionsBuilder =>
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// إضافة خدمات استكشاف النقاط النهائية (Endpoints) وSwagger للتوثيق
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


AutoMapperExtension.ConfigureServices(builder.Services);

builder.Services.AddScoped<FileService>();
builder.Services.AddControllers(); // إضافة خدمات التحكم (Controllers) إلى التطبيق

var app = builder.Build(); // بناء التطبيق

// تكوين خط أنابيب معالجة الطلبات HTTP
if (app.Environment.IsDevelopment()) // إذا كان التطبيق في بيئة التطوير
{
    app.UseSwagger(); // تفعيل Swagger
    app.UseSwaggerUI(); // تفعيل واجهة مستخدم Swagger
}

app.UseHttpsRedirection(); // إعادة توجيه الطلبات إلى HTTPS

app.MapControllers(); // تعيين مسارات النقاط النهائية للتحكم

app.UseStaticFiles();

app.Run(); // تشغيل التطبيق
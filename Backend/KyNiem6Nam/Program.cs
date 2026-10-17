using KyNiem6Nam._02_Services._01_Interface;
using KyNiem6Nam._02_Services._02_Implementation;
using KyNiem6Nam._03_Repositories._01_Interface;
using KyNiem6Nam._03_Repositories._02_Implementation;
using KyNiem6Nam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
// Thêm các using tới thư mục Models, Repositories, Services của bạn sau khi tạo
// using YourProjectName.Models;
// using YourProjectName.Repositories;
// using YourProjectName.Services;

var builder = WebApplication.CreateBuilder(args);

// ================= DbContext =================
builder.Services.AddDbContext<KyNiem6NamContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ================= Repository (Mẫu cho project kỷ niệm) =================
// Tạm comment, khi nào bạn tạo các file Interface và Class thì mở ra
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
// builder.Services.AddScoped<ILetterRepository, LetterRepository>();
// builder.Services.AddScoped<ISavedPlaceRepository, SavedPlaceRepository>();
// builder.Services.AddScoped<IMusicRepository, MusicRepository>();
// builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();

// ================= Service (Mẫu cho project kỷ niệm) =================
builder.Services.AddScoped<IAuthService, AuthService>();
// builder.Services.AddScoped<ILetterService, LetterService>();
// builder.Services.AddScoped<ISavedPlaceService, SavedPlaceService>();
// builder.Services.AddScoped<IMusicService, MusicService>();
// builder.Services.AddScoped<IPhotoService, PhotoService>();

// ================= CORS (Phần Backend kết nối Frontend Angular) =================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Mở cửa cho Angular
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Rất quan trọng: Phải có dòng này thì sau này tính năng Vị trí (SignalR) mới hoạt động được
    });
});

// ================= Controller =================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 1. Kích hoạt CORS (Bắt buộc phải đứng trước UseAuthorization)
app.UseCors("AllowAngular");

// 2. Cấu hình Frontend tĩnh / Thư mục Ảnh
// Tạo và mở thư mục "Uploads" để lưu ảnh kỷ niệm, cho phép Angular lấy URL ảnh hiển thị
var imagePath = Path.Combine(builder.Environment.ContentRootPath, "Uploads");
if (!Directory.Exists(imagePath))
{
    Directory.CreateDirectory(imagePath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagePath),
    RequestPath = "/Uploads",
    OnPrepareResponse = ctx =>
    {
        // Cho phép Frontend Angular tải ảnh mà không bị lỗi CORS
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:4200");
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
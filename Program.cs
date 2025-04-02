using Microsoft.EntityFrameworkCore;
using WebApplicationPR.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("RecService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["RecService:BaseUrl"]);
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
});
builder.Services.AddHttpClient("TaskService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["TaskService:BaseUrl"]);
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
});
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // адрес твоего фронта
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
// app.Urls.Add("http://+:8081");

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();
app.Run();
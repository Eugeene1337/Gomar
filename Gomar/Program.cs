using Gomar.Models;
using Gomar.Models.MappingProfiles;
using Gomar.Services;
using Gomar.Services.Interfaces;
using Gomar.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<DatabaseSettings>>().Value);
builder.Services.Configure<AdminUser>(builder.Configuration.GetSection(nameof(AdminUser)));
builder.Services.AddSingleton<IAdminUser>(x => x.GetRequiredService<IOptions<AdminUser>>().Value);

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IMontageService, MontageService>();
builder.Services.AddScoped<ITextService, TextService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddAutoMapper(typeof(MontageMappingProfile));
builder.Services.AddAutoMapper(typeof(ProductMappingProfile));

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme
).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    options =>
    {
        options.LoginPath = "/Admin";
        options.LogoutPath = "/Logout";
    });
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

builder.Services.AddTransient<UserManager>();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddControllersWithViews();

ConfigurationHelper.Initialize(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
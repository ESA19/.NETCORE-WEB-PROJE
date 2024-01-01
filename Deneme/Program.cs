using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Deneme.Data;
using Deneme.Areas.Identity.Data;
using FluentAssertions.Common;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DenemeDbContextConnection") ?? throw new InvalidOperationException("Connection string 'DenemeDbContextConnection' not found.");


builder.Services.AddDbContext<DenemeDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<DenemeDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "Resources";
});
builder.Services.AddMvc().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var cultures = new List<CultureInfo>
    {
        new CultureInfo("en"),
        new CultureInfo("tr"),

    };
    options.DefaultRequestCulture = new RequestCulture("tr");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;


});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
//var supportedCultures = new[] { "en", "tr"};
//var localizationOptions=new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[1]).AddSupportedUICultures(supportedCultures).AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(app.Services.CreateScope().ServiceProvider.
    GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
using (var scope=app.Services.CreateScope())
{
    await DbSeeder.SeedRolesAndAdminAsync(scope.ServiceProvider);
}

app.Run();

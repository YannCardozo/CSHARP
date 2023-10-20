using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMvc.Data;
using System.Configuration;
using Microsoft.EntityFrameworkCore.Sqlite;
using Pomelo.EntityFrameworkCore.MySql;
using Org.BouncyCastle.Crypto.Engines;
using SalesWebMvc.Services;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);


//ATIVAR O BUILDER PARA UTILIZAR SQLSERVER

//builder.Services.AddDbContext<SalesWebMvcContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("SalesWebMvcContext") ?? throw new InvalidOperationException("Connection string 'SalesWebMvcContext' not found.")));

//MYSQL SERVER
//builder.Services.AddDbContext<SalesWebMvcContext>(options =>
//{
//    options.UseMySQL(Configuration.GetConnectionString("SalesWebMvcContext"),
//        new MySqlServerVersion(new Version(8, 0, 26)), // Especifique a versão do MySQL correta
//        builder => builder.MigrationsAssembly("SalesWebMvc"));
//});

var connectionString = builder.Configuration.GetConnectionString("SalesWebMvcContext");
builder.Services.AddDbContext<SalesWebMvcContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));


});


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartmentService>();
var app = builder.Build();



var ptbr = new CultureInfo("pt-br");
var LocalizationOption = new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(ptbr),
    SupportedCultures = new List<CultureInfo> { ptbr },
    SupportedUICultures = new List<CultureInfo> { ptbr },
};
app.UseRequestLocalization(LocalizationOption);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

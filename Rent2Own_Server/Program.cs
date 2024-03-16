using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Rent2Own_DataAccess;
using Rent2Own_Server.Service;
using Rent2Own_Server.Service.IService;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Identity;
using Rent2Own_Business.Interfaces;
using Rent2Own_Business;
using Stripe;

//License for Syncfusion
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF1cWWhIYVdpR2Nbe05yflZBal9WVBYiSV9jS31SdEVmWHtdd3ddQmhYVw==");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddScoped<IOrderOperations, OrderOperations>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddDbContext<DBHelper>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddDefaultUI()
    .AddEntityFrameworkStores<DBHelper>();

var app = builder.Build();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe")["ApiKey"];

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage(); 
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

SeedDatabase();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
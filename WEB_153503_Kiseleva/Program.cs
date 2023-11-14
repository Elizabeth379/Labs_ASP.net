using Microsoft.AspNetCore.Authentication;
using NuGet.Packaging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WEB_153503_Kiseleva.Models;
using WEB_153503_Kiseleva.Services.CategoryService;
using WEB_153503_Kiseleva.Services.ProductService;
using Microsoft.AspNetCore.Cors.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICategoryService, ApiCategoryService>();
builder.Services.AddScoped<IProductService, ApiProductService>();


UriData uriData = builder.Configuration.GetSection("UriData").Get<UriData>()!;

builder.Services.AddHttpClient<IProductService, ApiProductService>(opt => opt.BaseAddress = new Uri(uriData.ApiUri));
builder.Services.AddHttpClient<ICategoryService, ApiCategoryService>(opt => opt.BaseAddress = new Uri(uriData.ApiUri));


builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "cookie";
    opt.DefaultChallengeScheme = "oidc";
    opt.DefaultChallengeScheme = "bearer";
})
.AddCookie("cookie")
.AddJwtBearer("bearer")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority =
    builder.Configuration["IdentityServerSettings:AuthorityUrl"];
    options.ClientId =
    builder.Configuration["IdentityServerSettings:ClientId"];
    options.ClientSecret =
    builder.Configuration["IdentityServerSettings:ClientSecret"];

    options.GetClaimsFromUserInfoEndpoint = true;
    options.ResponseType = "code";
    options.ResponseMode = "query";

    options.TokenValidationParameters.NameClaimType = "name";
    options.TokenValidationParameters.RoleClaimType = "role";

    //options.Scope.Add(builder.Configuration["IdentityServerSettings:ClientId"]!);
    options.Scope.Add(builder.Configuration["IdentityServerSettings:Scopes"]!);
    //List<string> sc = new() { "WEB", "openid", "profile", "email" };
    //options.Scope.AddRange<string>(sc);
    options.SaveTokens = true;
});

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("admin", b => b.RequireClaim("role", "admin"));
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
app.MapRazorPages();//.RequireAuthorization();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



using Serilog;
using Serilog.Events;
using WEB_153503_KISELEVA;
using WEB_153503_KISELEVA.Helpers;
using WEB_153503_KISELEVA.Services.CartService;
using WEB_153503_KISELEVA.Services.CategoryService;
using WEB_153503_KISELEVA.Services.ProductService;
using WEB_153503_KISELEVA.TagHelpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<PagerTagHelper>();

builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

// builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
// builder.Services.AddScoped<IProductService, MemoryProductService>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "cookie";
    opt.DefaultChallengeScheme = "oidc";
})
    .AddCookie("cookie", options =>
    {
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority =
        builder.Configuration["InteractiveServiceSettings:AuthorityUrl"];
        options.ClientId =
        builder.Configuration["InteractiveServiceSettings:ClientId"];
        options.ClientSecret =
        builder.Configuration["InteractiveServiceSettings:ClientSecret"];
        // Получить Claims пользователя
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ResponseType = "code";
        options.ResponseMode = "query";
        options.Scope.Add("api.read");
        options.Scope.Add("api.write");
        options.SaveTokens = true;
    });

builder.Services.AddDistributedMemoryCache();


var configuration = builder.Configuration;

var uriData = new UriData
{
    ApiUri = configuration.GetSection("UriData").GetValue<string>("ApiUri")!,
    ISUri = configuration.GetSection("UriData").GetValue<string>("ISUri")!
};

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddHttpClient<IProductService, ApiProductService>(opt =>opt.BaseAddress = new Uri(uriData.ApiUri));
builder.Services.AddHttpClient<ICategoryService, ApiCategoryService>(opt =>opt.BaseAddress = new Uri(uriData.ApiUri));

builder.Services.AddScoped(SessionCart.GetCart);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration).Filter.ByIncludingOnly(evt =>
    {
        if (evt.Properties.TryGetValue("StatusCode", out var statusCodeValue) &&
            statusCodeValue is ScalarValue statusCodeScalar &&
            statusCodeScalar.Value is int statusCode)
        {
            return statusCode < 200 || statusCode >= 300;
        }
        return false;
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseLoggingMiddleware();

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();


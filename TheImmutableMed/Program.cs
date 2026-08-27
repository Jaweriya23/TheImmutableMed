using TheImmutableMed.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC only
builder.Services.AddControllersWithViews();

// Keep memory cache (optional but ok)
builder.Services.AddMemoryCache();

// Your services (optional)
builder.Services.AddSingleton<TriageService>();
builder.Services.AddScoped<LegacyAdapter>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
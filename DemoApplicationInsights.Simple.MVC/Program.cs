using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using DemoApplicationInsights.Simple.MVC.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(o =>
{
    o.Filters.Add<PageViewActionFilterAttribute>();
});
builder.Services.AddApplicationInsightsTelemetry(o =>
{
    o.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
});

builder.Services.AddSingleton(typeof(TelemetryClient), new TelemetryClient(new TelemetryConfiguration()));

builder.Services.AddHealthChecks()
    .AddApplicationInsightsPublisher(builder.Configuration["ApplicationInsights:InstrumentionKey"]);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHealthChecks("/health");

app.Run();

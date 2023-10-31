using BlazorOdata.Server.EfCore;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseSqlServer("Data Source=.;Initial Catalog=BlazorHero.CleanArchitecture;Integrated Security=SSPI;MultipleActiveResultSets=True;TrustServerCertificate=True"));


builder.Services.AddControllers().AddOData(options =>
{
    options.AddRouteComponents(routePrefix: "odata", CustomEdmModel.GetEdmModel())
        .Select()
        .Count()
        .Filter()
        .Expand()
        .OrderBy()
        .SetMaxTop(maxTopValue: 100);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

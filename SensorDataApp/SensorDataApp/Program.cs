using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SensorDataApp.Data;
using SensorDataApp.Hubs;
using SensorDataApp.Model;
using SensorDataApp.Service;
using SQLite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

SQLiteAsyncConnection con = new SQLiteAsyncConnection(@".\sensordata.db");
con.CreateTableAsync<DataPoint>();
con.CreateTableAsync<Sensor>();

builder.Services.AddControllers();
builder.Services.AddSingleton<DataService>(new DataService(con));
builder.Services.AddSingleton<SensorService>(new SensorService());
builder.Services.AddSingleton<SQLiteAsyncConnection>(con);

builder.Services.AddSignalR();

builder.Services.AddMatBlazor();

if (builder.Services.All(x => x.ServiceType != typeof(HttpClient)))
{
    builder.Services.AddScoped(
        s =>
        {
            var navigationManager = s.GetRequiredService<NavigationManager>();
            return new HttpClient
            {
                BaseAddress = new Uri(navigationManager.BaseUri)
            };
        });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapHub<SensorsHub>("/sensorshub");
app.MapHub<SensorDataHub>("/sensordatahub");

app.MapFallbackToPage("/_Host");

app.MapControllers();

app.Run();

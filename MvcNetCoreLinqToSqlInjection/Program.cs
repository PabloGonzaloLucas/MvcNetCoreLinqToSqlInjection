using MvcNetCoreLinqToSqlInjection.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddTransient<Coche>();
Coche car = new Coche();
car.Marca = "Ferrari";
car.Modelo = "TO GUAPO";
car.Imagen = "ferrari.jpg";
car.Velocidad = 0;
car.VelocidadMaxima = 200;
builder.Services.AddSingleton<ICoche, Coche>(x => car);
//builder.Services.AddSingleton<ICoche, Coche>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WesternInn_AF_HLN_HP.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source = WesternInn.db"));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

/* The code block below runs the CreateRoles() method.
 * 
 * The using () statement is for garbage collection.
 * Specifically, it defines a scope for created objects.
 * If outside this scope, the objects will be destroyed.
 */
using(var scope = app.Services.CreateScope())
{
    //get the services providers
    var services = scope.ServiceProvider;
    try
    {
        var serviceProvider = services.GetRequiredService<IServiceProvider>();
        //get the config object for the appsettings.json file
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        //calling the static method
        SeedRoles.CreateRoles(serviceProvider, configuration).Wait();
    }
    catch(Exception exception)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "An error occurred while creating roles");
    }
}


app.Run();

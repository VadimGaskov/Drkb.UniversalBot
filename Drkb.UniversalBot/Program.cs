using Drkb.JwtConfiguration.DI;
using Drkb.UniversalBot.Infrastructure.Data;
using Drkb.UniversalBot.Infrastructure.DI;
using Drkb.UniversalBot.Infrastructure.LoggerConfiguration;
using Drkb.UniversalBot.Infrastructure.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCorsPolicies();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddJwtGeneration();
builder.Services.AddBehavior();
builder.Services.AddMediatr();
builder.Services.AddDataProviderServices();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddQueryObjects();
builder.Services.AddSwagger();

builder.Services.AddSerilogLogger();
Log.Logger = SerilogConfiguration.GetSerilogConfiguration(builder.Configuration);
builder.Host.UseSerilog();
builder.Services.AddRabbitMQCollection(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger()
        .UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Drkb.UniversalBot");
            options.RoutePrefix = string.Empty;
        });
}

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BotDbContext>();
    await dbContext.Database.MigrateAsync();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AllowFrontend");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CheckMaxHeaderMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
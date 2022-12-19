using PKIK_Project.Configurations;
using PKIK_Project.Helpers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

if (!builder.Environment.IsDevelopment())
{
    var port = Environment.GetEnvironmentVariable("PORT");
    builder.WebHost.UseUrls("http://*:" + port);
}

builder.Services.AddHealthChecks();
builder.Services.RegisterValidators();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterSwagger();
builder.Services.RegisterScrutor();
builder.Services.RegisterContextAccessor();
builder.Services.RegisterDbContext(builder.Configuration);
builder.Services.RegisterIdentity();
builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.RegisterMapper();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (/*app.Environment.IsDevelopment()*/true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseReDoc(c =>
    {
        c.DocumentTitle = "API Documentation";
        c.SpecUrl = "/swagger/v1/swagger.json";
    });
}

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

app.MapGet("/version", async context =>
{
    var version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
    await context.Response.WriteAsync(version);
});
app.MapHealthChecks("/health");
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();



public partial class Program { }


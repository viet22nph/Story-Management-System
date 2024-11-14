using Application.DependencyInjections.Extensions;
using OnlineStory.Api.Extensions;
using OnlineStory.Infrastructure.DependencyInjection.Extensions;
using OnlineStory.Infrastructure.MessageQueue.DependencyInjection.ExtensionsExtensions;
using OnlineStory.Persistence.ApplicationDbContext;
using Persistence.DependencyInjections.Extentions;
using Persistence.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Controller API

builder.Services.AddScoped<SeedData>();
builder.Services
    .AddControllers()
    .AddApplicationPart(OnlineStory.Presentation.AssemblyReference.Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddServerSqlPersistent();
builder.Services.AddIdentityPersister();
builder.Services.AddRepositoryPersistence();
builder.Services.AddServicePersistence();
builder.Services.AddMediatRApplication();

builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddMasstransitRabbitMQServices(builder.Configuration);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
                      });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseAuthentication();

app.UseCors(MyAllowSpecificOrigins).UseForwardedHeaders();

app.UseStaticFiles();
app.MapControllers();

//Seed
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    var seed = services.GetService<SeedData>();
    if (seed != null)
    {
        await seed.SeedDataAsync();

    }
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}
app.Run();
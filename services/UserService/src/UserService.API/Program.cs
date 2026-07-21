using UserService.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// Register Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply EF Core migrations
app.ApplyDatabaseMigrations();

// Configure HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
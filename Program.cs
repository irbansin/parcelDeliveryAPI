using Microsoft.EntityFrameworkCore;
using ParcelDelivery.Api.DAO;
using ParcelDelivery.Api.Data;
using ParcelDelivery.Api.Interfaces;
using ParcelDelivery.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// parcel classifier (business rules service)
builder.Services.AddSingleton<IParcelClassifier, ParcelClassifier>();
// DAOs
builder.Services.AddScoped<IDepartmentDao, DepartmentDao>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
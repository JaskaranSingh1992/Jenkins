using Jenkins.Db;
using Jenkins.Repository;
using Jenkins.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure in memory database
builder.Services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("HRMDB"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Register DI
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();


var app = builder.Build();


// 2. Find the service within the scope to use
using (var scope = app.Services.CreateScope())
{
    // 3. Get the instance of HRMContext in our service layer
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<Context>();

    // 4. Call the SeedDataGenerator to generate seed data
    SeedDataGenerator.Initialize(services);
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

// To show both development and deployment
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

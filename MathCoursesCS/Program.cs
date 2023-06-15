var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseRouting(); // Make sure this line is before other middleware components

//// Other middleware components...

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers(); // Add this line to map the controllers
//});


app.UseAuthorization();

app.MapControllers();

app.Run();

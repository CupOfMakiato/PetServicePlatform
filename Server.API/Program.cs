using Server.API;
using Server.Application;
using Server.Domain.Entities;
using Server.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services
        .AddApi()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}


builder.Services.AddControllers();

builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pet Service Management Web API v1");
        c.InjectJavascript("/custom-swagger.js");
    });

}

app.UseExceptionHandler("/Error");

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

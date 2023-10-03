using Microsoft.EntityFrameworkCore;
using SMSSendingService.Data;
using SMSSendingService.Automapper;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SMSSendingServiceContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SMSSendingServiceContext") ?? throw new InvalidOperationException("Connection string 'SMSSendingServiceContext' not found.")));

var mapper = AutoMapperConfig.Initialize();

builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                      });
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseCors(options =>
                     options.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();

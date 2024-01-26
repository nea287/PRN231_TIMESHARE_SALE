using Microsoft.EntityFrameworkCore;
using PRN231_TIMESHARE_SALES_API.AppStarts;
using PRN231_TIMESHARE_SALES_DataLayer.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DbContext
builder.Services.AddDbContext<PRN231_TimeshareSalesDBContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(builder.Configuration.GetConnectionString("TimeShareSalesDB"));
});
#endregion

#region AppStarts
builder.Services.AddAutoMapper(typeof(AutoMapperResolver).Assembly);
builder.Services.ConfigDI();
#endregion

#region CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigins", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

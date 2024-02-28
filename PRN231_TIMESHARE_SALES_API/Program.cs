using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PRN231_TIMESHARE_SALES_API.AppStarts;
using PRN231_TIMESHARE_SALES_DataLayer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    #region JWT
    //Khai bao bearer token trong swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        BearerFormat = "JWT",
        Description = "Enter your token",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        #region Token Bearer
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
        #endregion
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                securityScheme,
                new string[] {"Bearer"} //new string[] {} Như nhau
            }
        });
    #endregion
});

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

#region Authen and Author

var tokenConfig = builder.Configuration.GetSection("Token");
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // clear default behaviour không ảnh hưởng đến jwt
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig["SecretKey"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        //ClockSkew = TimeSpan.Zero // remove delay of token when expire
    };
})
.AddJwtBearer("RefreshToken", options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig["RefreshTokenSecretKey"])),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

//authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
    {
        policy.RequireRole("ADMIN");
    });
    options.AddPolicy("RequireStaffRole", policy =>
    {
        policy.RequireRole("STAFF");
    });
    options.AddPolicy("Bearer", policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});

#endregion

#region CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigins", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
#endregion
#region Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisCacheUrl"];
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
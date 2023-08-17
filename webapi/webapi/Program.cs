using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using webapi.Data;
using webapi.utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Localhost");

// Add services to the container.
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var corsPolicy = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
                      policy =>
                      {
                          policy.AllowAnyOrigin()    //��������Origin����
                          .AllowAnyMethod()    //�����������󷽷���Get,Post,Put,Delete
                          .AllowAnyHeader();   //������������ͷ:application/json
                      });
});

//ע��JWT����
var configuration = builder.Configuration;
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false, //�Ƿ���֤Issuer
        ValidIssuer = configuration["Jwt:Issuer"], //������Issuer
        ValidateAudience = false, //�Ƿ���֤Audience
        ValidAudience = configuration["Jwt:Audience"], //������Audience
        ValidateIssuerSigningKey = true, //�Ƿ���֤SecurityKey
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])), //SecurityKey
        ValidateLifetime = true, //�Ƿ���֤ʧЧʱ��
        ClockSkew = TimeSpan.FromSeconds(2), //����ʱ���ݴ�ֵ�������������ʱ�䲻ͬ�����⣨�룩
        RequireExpirationTime = true,
    };
});
builder.Services.AddSingleton(new JwtHelper(configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicy);

//�����м����UseAuthentication����֤����������������Ҫ�����֤���м��ǰ���ã����� UseAuthorization����Ȩ����
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

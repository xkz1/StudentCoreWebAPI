using System.Text;

using CoreWebAPI;

using DAL;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Panda.DynamicWebApi;

using static DemoJWT.Authorization.JwtHelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//序列化
builder.Services.AddControllers().AddJsonOptions(p => p.JsonSerializerOptions.PropertyNamingPolicy = null);
//跨域
builder.Services.AddCors((x) => { x.AddPolicy("kuayu", d => d.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); });
//连接数据库字符串
builder.Services.AddDbContext<MyDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

#region Token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = TokenParameter.Issuer,
        ValidAudience = TokenParameter.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenParameter.Secret))
    };
});

//token验证
//builder.Services.AddControllers(/* 或过 AddMvc() */).AddMvcOptions(options =>
//{
//    options.Filters.Add(new TokenValidationFilter());
//});

#endregion




//builder.Services.AddDynamicWebApi();  //这个会执行路由，多次执行会造成路由重复
builder.Services.AddSwaggerGen(c =>
          {
              //文档注释
              c.SwaggerDoc("v2", new OpenApiInfo() { Title = "My API", Version = "v1" });
              c.SwaggerDoc("v1", new OpenApiInfo()
              { Title = "学生管理系统API", Version = "v1" });

              // TODO:一定要返回true！
              c.DocInclusionPredicate((docName, description) => true);

              var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
              var xmlFile = $"{AppDomain.CurrentDomain.FriendlyName}.xml"; // 简化字符串拼接
              var xmlPath = Path.Combine(baseDirectory, xmlFile);
              if (File.Exists(xmlPath)) // 检查文件是否存在
              {
                  c.IncludeXmlComments(xmlPath);
              }


          }
          );
builder.Services.AddScoped<Startup>();

#region Swagger分组


#endregion

builder.Services.AddMvc();
// 添加动态WebApi 需放在 AddMvc 之后
builder.Services.AddDynamicWebApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(
c =>
{
    //文档注释
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V2");
}
);
app.UseStaticFiles();
app.UseCors("kuayu");
//}

app.UseAuthorization();

app.MapControllers();

app.Run();

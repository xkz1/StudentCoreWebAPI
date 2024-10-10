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

//���л�
builder.Services.AddControllers().AddJsonOptions(p => p.JsonSerializerOptions.PropertyNamingPolicy = null);
//����
builder.Services.AddCors((x) => { x.AddPolicy("kuayu", d => d.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); });
//�������ݿ��ַ���
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

//token��֤
//builder.Services.AddControllers(/* ��� AddMvc() */).AddMvcOptions(options =>
//{
//    options.Filters.Add(new TokenValidationFilter());
//});

#endregion




//builder.Services.AddDynamicWebApi();  //�����ִ��·�ɣ����ִ�л����·���ظ�
builder.Services.AddSwaggerGen(c =>
          {
              //�ĵ�ע��
              c.SwaggerDoc("v2", new OpenApiInfo() { Title = "My API", Version = "v1" });
              c.SwaggerDoc("v1", new OpenApiInfo()
              { Title = "ѧ������ϵͳAPI", Version = "v1" });

              // TODO:һ��Ҫ����true��
              c.DocInclusionPredicate((docName, description) => true);

              var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
              var xmlFile = $"{AppDomain.CurrentDomain.FriendlyName}.xml"; // ���ַ���ƴ��
              var xmlPath = Path.Combine(baseDirectory, xmlFile);
              if (File.Exists(xmlPath)) // ����ļ��Ƿ����
              {
                  c.IncludeXmlComments(xmlPath);
              }


          }
          );
builder.Services.AddScoped<Startup>();

builder.Services.AddMvc();
// ��Ӷ�̬WebApi ����� AddMvc ֮��
builder.Services.AddDynamicWebApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(
c =>
{
    //�ĵ�ע��
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V2");
}
);
app.UseStaticFiles();
app.UseCors("kuayu");
//}

app.UseAuthorization();

app.MapControllers();

app.Run();

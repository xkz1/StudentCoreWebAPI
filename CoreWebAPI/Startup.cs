using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Panda.DynamicWebApi;

namespace CoreWebAPI
{
    public class Startup
    {
        IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {


            var swaggerGroups = configuration.GetSection("SwaggerGroups").Get<List<SwaggerGroup>>();

            // 启用Swagger生成器
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo()
            //    { Title = "张志刚的私人电商API", Version = "v1" });

            //    // TODO:一定要返回true！
            //    c.DocInclusionPredicate((docName, description) => true);


            //    foreach (var group in swaggerGroups)
            //    {
            //        c.SwaggerDoc(group.Name, new Microsoft.OpenApi.Models.OpenApiInfo { Title = group.Title, Version = group.Version });
            //    }

            //    // 配置Swagger分组
            //    c.DocInclusionPredicate((docName, description) =>
            //    {
            //        var groupName = swaggerGroups.FirstOrDefault(g => g.Name == docName);
            //        return groupName != null;
            //    });

            //    // 添加XML注释
            //    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //    var xmlFile = $"{AppDomain.CurrentDomain.FriendlyName}.xml";
            //    var xmlPath = Path.Combine(baseDirectory, xmlFile);
            //    if (File.Exists(xmlPath))
            //    {
            //        c.IncludeXmlComments(xmlPath);
            //    }
            //});


            // 默认配置
            //services.AddDynamicWebApi();

            // 自定义配置
            services.AddDynamicWebApi((options) =>
            {
                // 指定全局默认的 api 前缀
                options.DefaultApiPrefix = "apis";

                /**
                 * 清空API结尾，不删除API结尾;
                 * 若不清空 CreatUserAsync 将变为 CreateUser
                 */
                options.RemoveActionPostfixes.Clear();

                /**
                 * 自定义 ActionName 处理函数;
                 */
                options.GetRestFulActionName = (actionName) => actionName;

                /**
                 * 指定程序集 配置 url 前缀为 apis
                 * 如: http://localhost:8080/apis/User/CreateUser
                 */
                options.AddAssemblyOptions(this.GetType().Assembly, apiPreFix: "apis");

                /**
                 * 指定程序集 配置所有的api请求方式都为 POST
                 */
                options.AddAssemblyOptions(this.GetType().Assembly, httpVerb: "POST");

                /**
                 * 指定程序集 配置 url 前缀为 apis, 且所有请求方式都为POST
                 * 如: http://localhost:8080/apis/User/CreateUser
                 */
                options.AddAssemblyOptions(this.GetType().Assembly, apiPreFix: "apis", httpVerb: "POST");
            });
            // 添加JWT身份验证服务
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     var secretByte = Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]);  // 从appsettings.json读取Jwt配置
                     options.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ValidateIssuer = true,
                         ValidIssuer = Configuration["Authentication:Issuer"],

                         ValidateAudience = true,
                         ValidAudience = Configuration["Authentication:Audience"],

                         ValidateLifetime = true,


                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(secretByte)
                     };
                 });



            //       services.AddSwaggerGen(options =>
            //{
            //    //遍历ApiGroupNames所有枚举值生成接口文档，Skip(1)是因为Enum第一个FieldInfo是内置的一个Int值
            //    typeof(ApiGroupNames).GetFields().Skip(1).ToList().ForEach(f =>
            //    {
            //        //获取枚举值上的特性
            //        var info = f.GetCustomAttributes(typeof(GroupInfoAttribute), false).OfType<GroupInfoAttribute>().FirstOrDefault();
            //        options.SwaggerDoc(f.Name, new Swashbuckle.AspNetCore.Swagger
            //        {
            //            Title = info?.Title,
            //            Version = info?.Version,
            //            Description = info?.Description
            //        });
            //    });
            //    //没有加特性的分到这个NoGroup上
            //    options.SwaggerDoc("NoGroup", new Swashbuckle.AspNetCore.Swagger.Info
            //    {
            //        Title = "无分组"
            //    });
            //    //判断接口归于哪个分组
            //    options.DocInclusionPredicate((docName, apiDescription) =>
            //    {
            //        if (docName == "NoGroup")
            //        {
            //            //当分组为NoGroup时，只要没加特性的都属于这个组
            //            return string.IsNullOrEmpty(apiDescription.GroupName);
            //        }
            //        else
            //        {
            //            return apiDescription.GroupName == docName;
            //        }
            //    });
            //    services.Configure<MvcOptions>(c =>
            //       {
            //           c.Conventions.Add(new DynamicWebApiConvention((ISelectController)services, null));
            //       });
            //}
        }
    }
}

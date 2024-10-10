using System.Net;
using System.Net.Mail;
using System.Text;

using BLL;

using DAL;

using DemoJWT.Authorization;

using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;

using Mapster;

using Model.Dto;
using Model.Entity;

using SqlSugar.Extensions;

using StackExchange.Redis;
namespace CoreWebAPI.ControllersSerivce
{
    //[Route("[User]/[action]")]
    public class UserAppService : IAppService
    {
        UserBll bll = new UserBll();
        static JwtHelper _jwtConfig = new JwtHelper(null);
        private readonly string _uploadPath; // 上传路径
        static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            EndPoints = { "localhost:6379" },
            Password = "123",
            DefaultDatabase = 1
        });
        IDatabase db = redis.GetDatabase();

        public UserAppService(IWebHostEnvironment Imgeng)
        {
            _uploadPath = Path.Combine(Imgeng.ContentRootPath, "wwwroot"); // 设置上传路径

            if (!Directory.Exists(_uploadPath)) // 检查目录是否存在
            {
                Directory.CreateDirectory(_uploadPath); // 创建目录
            }
        }

        #region 获取数据
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserAccount">用户账号</param>
        /// <param name="userPwd">用户密码</param>
        /// <returns></returns>
        //[IgnoreTokenVerification]
        public Res PostLogin(UserDtoOutToIn userin)
        {
            Res dto = new Res();
            try
            {
                (UserEntity, int) entity = bll.Login(userin.UserAccount, userin.UserPassword);

                if (entity.Item1 != null)
                {

                    Dictionary<string, object> payload = new Dictionary<string, object>();
                    payload["aud"] = entity.Item1.UserAccount;
                    payload["iss"] = "zzg";
                    //过期时间
                    payload["exp"] = Math.Ceiling((DateTime.UtcNow.AddDays(1) - new DateTime(1970, 1, 1)).TotalSeconds);
                    dto.Data = CreateJwtToken(payload);
                    return dto;
                }
                else
                {
                    if (entity.Item2 == -1)
                        dto.Message = "账号错误";
                    else
                        dto.Message = "密码错误";
                    return dto;
                }
            }
            catch (Exception ex)
            {
                dto.Message = ex.Message;
                dto.Data = null;
                return dto;
            }
        }
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Res GetUserList(string user)
        {
            Res dto = new Res();

            try
            {
                List<UserEntity> entity = bll.GetUserList(user);
                List<UserDto> Userdto = entity.Adapt<List<UserDto>>();
                dto.Data = Userdto;
                return dto;
            }
            catch (Exception ex)
            {
                dto.Code = 500;
                dto.Data = null;
                dto.Message = ex.Message;
                throw;
            }
        }
        /// <summary>
        /// 获取头像路径
        /// </summary>
        /// <param name="userAccount">用户名</param>
        /// <returns></returns>
        public Res GetAvatar(string userAccount)
        {
            Res dto = new Res();
            UserEntity? ph = bll.GetUserList(userAccount).FirstOrDefault();
            string? Avatar = ph?.UserAvatar;
            dto.Data = Avatar;
            return dto;
        }
        /// <summary>
        /// 验证码
        /// </summary>
        /// <param name="UserMailBox"></param>
        /// <returns></returns>
        public async Task<Res> GetCaptcha(string UserMailBox)
        {
            Res dto = new Res();
            var MailBox = bll.GetCaptcha(UserMailBox);
            bool isSuccess = db.StringSet(MailBox.Item1.UserAccount, MailBox.Item2, TimeSpan.FromMinutes(5));
            #region 邮箱发送
            //参考代码：http://www.luofenming.com/show.aspx?id=ART2017111400001

            // 创建一个邮件对象
            using var mailObject = new MailMessage();

            // 设置发件人邮箱
            mailObject.From = new MailAddress(EmailModel.FromMial);

            // 设置收件人
            var recipientEmail = MailBox.Item1.UserMailBox.ToString();
            mailObject.To.Add(new MailAddress(recipientEmail));

            // 设置邮件主题和内容
            mailObject.SubjectEncoding = Encoding.UTF8;
            mailObject.Subject = EmailModel.Subject;
            mailObject.BodyEncoding = Encoding.UTF8;
            var content = $"验证码是：{MailBox.Item2}";
            mailObject.Body = content;

            // 创建一个发送邮件的客户端对象
            using var smtpClient = new SmtpClient(EmailModel.ServerAddress, 465)
            {
                Credentials = new NetworkCredential(EmailModel.UserID, EmailModel.UserPwd),
                EnableSsl = false // SSL
            };

            // 发送邮件
            await smtpClient.SendMailAsync(mailObject);

            #endregion



            if (isSuccess)
            {
                dto.Data = 1;
                return dto;
            }
            else
            {
                dto.Message = "失败";
                dto.Data = 0;
                return dto;
            }

        }

        #endregion
        #region 提交数据
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Res PostIncreaseUser(UserDtoOutToIn entity)
        {
            Res res = new Res();
            try
            {
                var m = entity.Adapt<UserDtoOutToIn, UserEntity>();

                res.Data = bll.AddUser(m);
                if (res.Data.ObjToInt() == 0)
                {
                    res.Message = "邮箱重复";
                }
                return res;
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Message = ex.Message;
                res.Code = -201;
                return res;
            }

        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="Account">用户名</param>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        public Res UpdateUptPassword(UserPutPasswoed userDot)
        {

            Res res = new Res();
            try
            {
                int count = bll.UpdateUser(userDot.Account, userDot.oldPwd, userDot.newPwd);
                if (count == 1)
                {
                    res.Data = count;
                    return res;
                }
                else if (count == 2)
                {
                    res.Data = null;
                    res.Message = "缺少密码";
                    res.Code = -201;
                }
                else if (count == 3)
                {
                    res.Data = null;
                    res.Message = "用户不存在";
                    res.Code = -201;
                }
                else
                {
                    res.Data = null;
                    res.Message = "原密码错误";
                    res.Code = -201;
                }
                return res;
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Message = ex.Message;
                res.Code = -201;
                return res;
            }

        }

        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="UserMailBox"></param>
        /// <returns></returns>
        public Res GetForgotPassword(UserEntity user)
        {
            Res dto = new Res();
            dto.Data = bll.GetForgotPassword(user);
            if (dto.Data.ObjToInt() == 0)
            {
                dto.Message = "失败";
                return dto;
            }
            else
            {
                return dto;
            }
        }

        #endregion
        #region 私有方法
        /// <summary>
        /// 创建token
        /// </summary>
        /// <returns></returns>
        public static string CreateJwtToken(IDictionary<string, object> payload, string secret = "123")
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(payload, secret);
            return token;
        }
        /// <summary>
        /// 校验解析token
        /// </summary>
        /// <returns></returns>
        public static string ValidateJwtToken(string token, string secret = "123")
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm alg = new HMACSHA256Algorithm();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, alg);
                var json = decoder.Decode(token, secret, true);
                //校验通过，返回解密后的字符串
                return json;
            }
            catch (TokenExpiredException)
            {
                //表示过期
                return "ToKen过期";
            }
            catch (SignatureVerificationException)
            {
                //表示验证不通过
                return "ToKen验证不通过";
            }
            catch (Exception)
            {
                return "error";
            }
        }
        /// <summary>
        /// 上传用户头像图片
        /// </summary>
        /// <param name="file">图片</param>
        /// <param name="userAccount">用户账号</param>
        /// <returns></returns>
        public async Task<Res> PostUploadAvatar(IFormFile file, string userAccount)
        {
            Res Dto = new Res();

            try
            {
                if (file == null || file.Length == 0)
                {
                    Dto.Message = "没有选择图片";
                    return Dto;
                }

                var uploads = Path.Combine("wwwroot");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                // 使用用户名作为文件名
                var filePath = Path.Combine(uploads, $"{userAccount}.png");

                // 如果文件已存在，删除旧文件
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // 查询现有用户记录
                var existingImage = MyDbContext.SqlServerDb.Queryable<UserEntity>().Single(u => u.UserAccount == userAccount);

                if (existingImage.UserAvatar != null)
                {
                    // 更新用户头像路径
                    existingImage.UserAvatar = $"{userAccount}.png";

                    // 更新数据库记录
                    var updateResult = MyDbContext.SqlServerDb.Updateable<UserEntity>(existingImage)
                        .UpdateColumns(x => new { x.UserAvatar })
                        .Where(x => x.UserAccount == userAccount)
                        .ExecuteCommand();

                    if (updateResult > 0)
                    {
                        Dto.Message = "图片更新成功";
                        return Dto;
                    }
                    else
                    {
                        Dto.Code = 500;
                        Dto.Message = "更新数据库失败";
                        return Dto;
                    }
                }
                else
                {
                    // 用户没有上传过图片
                    var newImage = new UserEntity { UserAccount = userAccount, UserAvatar = filePath };
                    var uptResult = MyDbContext.SqlServerDb.Updateable<UserEntity>(newImage)
                        .UpdateColumns(x => new { x.UserAvatar })
                        .Where(x => x.UserAccount == userAccount)
                        .ExecuteCommand();

                    if (uptResult > 0)
                    {
                        Dto.Message = "图片上传成功";
                        return Dto;
                    }
                    else
                    {
                        Dto.Code = 500;
                        Dto.Message = "插入数据库失败";
                        return Dto;
                    }
                }
            }
            catch (Exception ex)
            {
                // 捕获并记录异常
                Dto.Code = 500;
                Dto.Message = $"发生错误: {ex.Message}";
                return Dto;
            }
        }
        #endregion
    }

}


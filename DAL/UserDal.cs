

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Microsoft.IdentityModel.Tokens;

using Model.Entity;


namespace DAL
{
    public class UserDal
    {

        #region 获取方法
        /// <summary>
        /// 根据 用户真实姓名 & 用户账号 & 用户邮箱 返回指定数据
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<UserEntity> GetUserList(string user)
        {
            if (!string.IsNullOrEmpty(user))
            {
                var d = MyDbContext.SqlServerDb.Queryable<UserEntity>().Where(x => x.Logic == 0).ToList();
                return d;
            }
            else
            {
                var r = MyDbContext.SqlServerDb.Queryable<UserEntity>().Where(x => x.Logic == 0).Where(x => x.UserMailBox == user || x.UserName == user || x.UserAccount == user);
                return r.ToList();
            }

        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserAccount">用户账号</param>
        /// <param name="userPwd">用户密码</param>
        /// <returns></returns>
        public (UserEntity, int) Login(string UserAccount, string userPwd)
        {
            string pwd = GetSha256Hash(userPwd);
            int errorCode = 0; // 初始错误代码为0，表示没有错误

            // 通过用户名查询用户
            UserEntity entity = MyDbContext.SqlServerDb.Queryable<UserEntity>()
                .Where(x => x.UserAccount.Equals(UserAccount))
                .Where(x => x.Logic == 0)
                .First();

            if (entity == null)
            {
                errorCode = -1;
            }
            else
            {
                // 验证密码是否正确
                if (userPwd == null || !entity.UserPassword.Equals(pwd))
                {

                    errorCode = -2;
                    entity = null; // 如果密码不正确，不应该返回任何用户信息
                }
            }

            return (entity, errorCode); // 返回元组，包含可能为null的用户实体和错误代码
        }
        public (UserEntity, int) GetCaptcha(string UserMailBox)
        {
            try
            {
                UserEntity ue = MyDbContext.SqlServerDb.Queryable<UserEntity>().WhereIF(UserMailBox != null, x => x.UserMailBox == UserMailBox).First();
                Random random = new Random();
                int randomNumber = random.Next(10000000, 100000000);
                return (ue, randomNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        #endregion

        #region 提交方法
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="m">UserEntity</param>
        /// <returns></returns>
        public int AddUser(UserEntity m)
        {
            int count = MyDbContext.SqlServerDb.Queryable<UserEntity>().Where(x => x.UserMailBox == m.UserMailBox).Count();
            if (count == 0)
            {
                m.UserPassword = GetSha256Hash(m.UserPassword);
                m.CreateTime = DateTime.Now;
                m.CreateUserId = "0";
                return MyDbContext.SqlServerDb.InsertableWithAttr(m).ExecuteCommand();
            }
            return 0;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="OldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        public int UpdateUser(string Account, string oldPwd, string newPwd)
        {
            //返回 1成功 2没有输入两个密码 3没有找到这个用户
            if (string.IsNullOrEmpty(oldPwd) || string.IsNullOrEmpty(newPwd))
            {
                return 2;
            }

            UserEntity? user = MyDbContext.SqlServerDb.Queryable<UserEntity>()
                 .Where(x => x.UserAccount == Account)
                 .First();

            if (user == null)
            {
                return 3;
            }
            if (user.UserPassword == GetSha256Hash(oldPwd))
                user.UserPassword = GetSha256Hash(newPwd);
            else
                return 4;

            int updateCount = MyDbContext.SqlServerDb.Updateable(user).ExecuteCommand();

            return updateCount;

        }
        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="UserMailBox"></param>
        /// <returns></returns>
        public int GetForgotPassword(UserEntity user)
        {
            try
            {
                UserEntity ue = MyDbContext.SqlServerDb.Queryable<UserEntity>().WhereIF(user != null, x => x.UserMailBox == user.UserMailBox).First();
                if (ue != null)
                {
                    ue.UserPassword = GetSha256Hash(user.UserPassword);
                    return MyDbContext.SqlServerDb.Updateable(ue).ExecuteCommand();
                }
                else
                {

                    return 0;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                throw;
            }
        }
        #endregion 

        #region 私有方法
        static string GetSha256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }


        #region Token
        /// <summary>
        /// jwt加密
        /// </summary>
        /// <param name="username"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public string GenerateJwtToken(string username, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, username)
        }),
                // 有效期设置为 7 天
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        #endregion
        #endregion




    }
}

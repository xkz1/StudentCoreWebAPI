using DAL;
using Model.Entity;

namespace BLL
{
    public class UserBll
    {
        UserDal dal = new UserDal();

        public int AddUser(UserEntity m)
        {
            return dal.AddUser(m);
        }
        public List<UserEntity> GetUserList(string user)
        {
            return dal.GetUserList(user);
        }
        /// <summary>
        /// 验证码
        /// </summary>
        /// <param name="UserMailBox"></param>
        /// <returns></returns>
        public (UserEntity, int) GetCaptcha(string UserMailBox)
        {
            return dal.GetCaptcha(UserMailBox);
        }
        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="UserMailBox"></param>
        /// <returns></returns>
        public int GetForgotPassword(UserEntity user) => dal.GetForgotPassword(user);
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserAccount">用户账号</param>
        /// <param name="userPwd">用户密码</param>
        /// <returns></returns>
        public (UserEntity, int) Login(string UserAccount, string userPwd)
        {
            return dal.Login(UserAccount, userPwd);
        }
        public int UpdateUser(string userName, string oldPwd, string newPwd)
        {
            return dal.UpdateUser(userName, oldPwd, newPwd);
        }
    }
}

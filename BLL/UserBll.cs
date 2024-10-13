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
        public (UserEntity, int) GetCaptcha(string UserMailBox) => dal.GetCaptcha(UserMailBox);
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserAccount">用户账号</param>
        /// <param name="userPwd">用户密码</param>
        /// <returns></returns>
        public (UserEntity, int) Login(string UserAccount, string userPwd) => dal.Login(UserAccount, userPwd);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        public int UpdateUser(string userName, string oldPwd, string newPwd) => dal.UpdateUser(userName, oldPwd, newPwd);
    }
}

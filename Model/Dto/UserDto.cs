namespace Model.Dto
{
    public class UserDto
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string? UserAccount { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string? UserMailBox { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string? UserAvatar { get; set; }
        /// <summary>
        /// 用户身份
        /// </summary>
        public string? UserIdentity { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public string? UserSex { get; set; }
        /// <summary>
        /// 用户班级
        /// </summary>
        public string? UserClass { get; set; }
    }
    public class UserDtoOutToIn : UserDto
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string? UserAccount { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string? UserPassword { get; set; }

    }
    #region OutToIn
    public class UserPutPasswoed
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 旧密码
        /// </summary>
        public string oldPwd { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string newPwd { get; set; }
    }
    #endregion
    #region InToOut

    #endregion
}

namespace Model.Dto
{
    /// <summary>
    /// 邮箱模板   --仅供参考
    /// </summary>
    public class EmailModel
    {

        /// <summary>
        /// 收件人邮箱
        /// </summary>
        public const string ToMial = "";
        /// <summary>
        /// 发件人邮箱
        /// </summary>
        public const string FromMial = "z2310988723@88.com";
        /// <summary>
        /// 发件人邮箱帐号 如QQ邮箱 为QQ号码
        /// </summary>
        public const string UserID = "z2310988723";
        /// <summary>
        /// 发件人邮箱受权码
        /// </summary>
        public const string UserPwd = "E8JHCqdvbdFbAn3p";
        /// <summary>
        /// 邮件服务地址 如QQ邮箱服务地址为smtp.qq.com
        ///            网易邮箱服务地址为smtp.163.com
        /// </summary>
        public const string ServerAddress = "smtp.88.com";
        /// <summary>
        /// 邮件主题/标题
        /// </summary>
        public const string Subject = "邮件验证码";
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Content;
        public const string FilePath = "";


    }
}

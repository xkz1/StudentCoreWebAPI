using SqlSugar;

namespace Model.Entity
{
    /// <summary>
    /// 用户
    /// </summary>
    [SugarTable("User")]
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(100)", ColumnDescription = "用户姓名")]
        public string? UserName { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(100)", ColumnDescription = "用户账号")]
        public string? UserAccount { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(100)", ColumnDescription = "用户密码")]
        public string? UserPassword { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(100)", ColumnDescription = "用户账号")]
        public string? UserMailBox { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(100)", ColumnDescription = "用户头像", IsNullable = true)]
        public string? UserAvatar { get; set; }
        /// <summary>
        /// 用户身份
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(10)", ColumnDescription = "用户身份", DefaultValue = "教师")]
        public string? UserIdentity { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(5)", ColumnDescription = "用户身份", IsNullable = true)]
        public string? UserSex { get; set; }
        /// <summary>
        /// 用户班级
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(5)", ColumnDescription = "用户班级", IsNullable = true)]
        public string? UserClass { get; set; }

    }
}

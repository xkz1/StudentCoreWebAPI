using SqlSugar;

namespace Model.Entity
{
    /// <summary>
    /// 班级
    /// </summary>
    [SugarTable("Sgrade")]
    public class SgradeEntity : BaseEntity
    {
        /// <summary>
        /// 年级
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(10)", ColumnDescription = "年级")]
        public string? Sgrades { get; set; }
        /// <summary>
        /// 班级
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(10)", ColumnDescription = "班级")]
        public string? SgradeClass { get; set; }
        /// <summary>
        /// 教室
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(10)", ColumnDescription = "教室")]
        public string? SgradeRoom { get; set; }
        /// <summary>
        /// 教师
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(150)", ColumnDescription = "教师")]
        public long SgradeTeacher { get; set; }
        /// <summary>
        /// 班级人数
        /// </summary>
        [SugarColumn(ColumnDataType = "int", ColumnDescription = "班级人数")]
        public int SgradeTotal { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(20)", ColumnDescription = "专业")]
        public string? SgradeSub { get; set; }


    }
}

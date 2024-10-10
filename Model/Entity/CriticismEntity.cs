using SqlSugar;

namespace Model.Entity
{
    /// <summary>
    /// 老师判题
    /// </summary>
    [SugarTable("Criticism")]
    public class CriticismEntity : BaseEntity
    {
        /// <summary>
        /// 题目
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(100)", ColumnDescription = "题目")]
        public long CriticismTopic { get; set; }
        /// <summary>
        /// 考试学生
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(100)", ColumnDescription = "考试学生")]
        public long CriticismStudent { get; set; }
        /// <summary>
        /// 学生答案
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(500)", ColumnDescription = "学生答案")]
        public string? CriticismAnswer { get; set; }
        /// <summary>
        /// 批判结果 0对1错
        /// </summary>
        [SugarColumn(ColumnDataType = "int", ColumnDescription = "批判结果 0对1错")]
        public int CriticismOutcome { get; set; }
        /// <summary>
        /// 老师指正 只有错误才有指正
        /// </summary>
        [SugarColumn(ColumnDataType = "int", ColumnDescription = "老师指正 只有错误才有指正", IsNullable = true)]
        public string? CriticismReason { get; set; }



    }
}

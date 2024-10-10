using SqlSugar;

namespace Model.Entity
{
    /// <summary>
    /// 考试记录
    /// </summary>
    [SugarTable("Exam")]
    public class ExamEntity : BaseEntity
    {
        /// <summary>
        /// 考试学生
        /// </summary>
        [SugarColumn(ColumnDataType = "long", ColumnDescription = "考试学生")]
        public long ExamStudent { get; set; }
        /// <summary>
        /// 考试场次(考试表)
        /// </summary>
        [SugarColumn(ColumnDataType = "long", ColumnDescription = "考试场次(考试表)")]
        public long ExamExamin { get; set; }


    }
}

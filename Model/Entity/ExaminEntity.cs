using SqlSugar;

namespace Model.Entity
{
    /// <summary>
    /// 考试表
    /// </summary>
    [SugarTable("Examin")]
    public class ExaminEntity : BaseEntity
    {
        /// <summary>
        /// 考试科目
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(20)", ColumnDescription = "考试科目")]
        public string ExaminSub { get; set; }
        /// <summary>
        /// 考试成绩
        /// </summary>
        [SugarColumn(ColumnDataType = "FLOAT", ColumnDescription = "考试成绩")]
        public double ExaminScores { get; set; }
        /// <summary>
        /// 考试所用时间
        /// </summary>
        [SugarColumn(ColumnDataType = "Time", ColumnDescription = "考试所用时间")]
        public TimeSpan ExaminTeam { get; set; }
        /// <summary>
        /// 考试开始时间
        /// </summary>
        [SugarColumn(ColumnDataType = "DateTime", ColumnDescription = "考试开始时间")]
        public DateTime ExaminBegin { get; set; }
        /// <summary>
        /// 考试结束时间
        /// </summary>
        [SugarColumn(ColumnDataType = "DateTime", ColumnDescription = "考试结束时间")]
        public DateTime ExaminEnd { get; set; }


    }
}

using SqlSugar;

namespace Model.Entity
{
    /// <summary>
    /// 题目表
    /// </summary>
    [SugarTable("Topic")]
    public class TopicEntity : BaseEntity
    {
        /// <summary>
        /// 题目
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(300)", ColumnDescription = "题目")]
        public int Topics { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(100)", ColumnDescription = "答案")]
        public string? TopicAnswer { get; set; }
        /// <summary>
        /// 分值
        /// </summary>
        [SugarColumn(ColumnDataType = "FLOAT", ColumnDescription = "分值")]
        public double TopicFraction { get; set; }
        /// <summary>
        /// 是否立即出题(是否老师批判)
        /// 0系统批判1老师批判
        /// </summary>
        [SugarColumn(ColumnDataType = "int", ColumnDescription = "0系统批判1老师批判")]
        public int TopicShow { get; set; }
        /// <summary>
        /// 是否批改完成 
        /// 0未批改1已批改
        /// </summary>
        [SugarColumn(ColumnDataType = "int", ColumnDescription = "批改 0未批改1已批改", DefaultValue = "0")]
        public int TopicFinish { get; set; }


    }
}

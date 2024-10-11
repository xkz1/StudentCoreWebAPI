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
        /// 标记正确答案
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(20)", ColumnDescription = "标记正确答案 1是正确", DefaultValue = "0")]
        public string TopicCorrect { get; set; }
        /// <summary>
        /// 所属类别
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(20)", ColumnDescription = "所属类别")]
        public string TopicSub { get; set; }
        /// <summary>
        /// 题型类别
        /// </summary>
        [SugarColumn(ColumnDataType = "int", ColumnDescription = "题型类别 1单选题2多选题3简答题")]
        public int TopicGory { get; set; }


    }
}

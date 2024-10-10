using SqlSugar;

namespace Model.Entity
{
    public class BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "主键")]
        public long Id { get; set; } = new Snowflake.Core.IdWorker(1, 1).NextId();
        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(100)", ColumnDescription = "创建人")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnDataType = "date", ColumnDescription = "创建时间")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改人
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(100)", ColumnDescription = "修改人", IsNullable = true)]
        public string UptUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [SugarColumn(ColumnDataType = "date", ColumnDescription = "创建时间", IsNullable = true)]
        public DateTime UptTime { get; set; }
        /// <summary>
        /// 逻辑删除 0 显示 1 不显示
        /// </summary>
        [SugarColumn(ColumnDataType = "int", ColumnDescription = "逻辑删除")]
        public int Logic { get; set; } = 0;
    }
}

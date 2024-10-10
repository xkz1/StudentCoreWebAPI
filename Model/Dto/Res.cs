namespace Model.Dto
{
    /// <summary>
    /// 返回操作结果集
    /// </summary>
    public class Res
    {
        public int Code { get; set; } = 200;
        public string Message { get; set; } = "操作成功";
        public object Data { get; set; }
    }
}

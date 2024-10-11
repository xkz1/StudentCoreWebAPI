using BLL;

using Model.Dto;
namespace CoreWebAPI.ControllersSerivce
{
    //[Route("[User]/[action]")]
    public class ExaminAppService : IAppService
    {
        ExaminBll bll = new ExaminBll();
        #region 获取数据
        /// <summary>
        /// 查询考试
        /// </summary>
        /// <param name="sub">科目</param>
        /// <param name="scores">成绩</param>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="topic">题目</param>
        /// <returns></returns>
        public Res GetQueayExam(string? sub = null, double scores = 0, DateTime? begin = null, DateTime? end = null, string topic = null)
        {
            Res dto = new Res();
            dto.Data = bll.QueayExam(sub, scores, begin, end, topic);
            return dto;
        }
        /// <summary>
        /// 查询题库
        /// </summary>
        /// <param name="examId">考试id</param>
        /// <returns></returns>
        public Res GetQueayTopic(string examId)
        {
            Res dto = new Res();
            dto.Data = bll.QueayTopic(examId);
            return dto;
        }
        #endregion
        #region 提交数据

        #endregion
        #region 私有方法

        #endregion

    }

}


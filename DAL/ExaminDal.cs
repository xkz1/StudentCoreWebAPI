using Model.Entity;

namespace DAL
{
    /// <summary>
    /// 考试
    /// </summary>
    public class ExaminDal
    {
        public ExaminDal() { }

        /// <summary>
        /// 查询考试
        /// </summary>
        /// <param name="sub">科目</param>
        /// <param name="scores">成绩</param>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="topic">题目</param>
        /// <returns></returns>
        public List<ExamEntity> QueayExam(string? sub = null, double scores = 0, DateTime? begin = null, DateTime? end = null, string topic = null)
        {
            List<ExamEntity> examEntities = MyDbContext.SqlServerDb.Queryable<ExamEntity>()

                .WhereIF(sub != null, x => x.ExaminSub == sub)
                .WhereIF(scores != 0, x => x.ExaminScores == scores)
                .WhereIF(begin != null, x => x.ExaminBegin >= begin)
                .WhereIF(end != null, x => x.ExaminEnd <= end).ToList();
            return examEntities;
        }
        /// <summary>
        /// 查询题库
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        public List<TopicEntity> QueayTopic(string examId)
        {
            // 查询改场考试下的题目Id
            string examTopics = MyDbContext.SqlServerDb.Queryable<ExamEntity>()
                .Where(x => x.Id.ToString() == examId)
                .Select(x => x.ExaminTopic) // 只选择 ExaminTopic 字段
                .First();

            // 将题目ID字符串转换为数组
            long[] topicIds = examTopics.Split(',').Select(long.Parse).ToArray();

            // 查询所有考题
            List<TopicEntity> topics = MyDbContext.SqlServerDb.Queryable<TopicEntity>()
                .In(topicIds.Select(id => (object)id).ToArray())
                .ToList();

            return topics;

        }
    }
}

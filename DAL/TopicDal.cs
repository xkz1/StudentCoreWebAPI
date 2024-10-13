using Model.Entity;

using SqlSugar;

namespace DAL
{
    /// <summary>
    /// 考试
    /// </summary>
    public class TopicDal
    {
        public TopicDal() { }
        /// <summary>
        /// 添加题目
        /// </summary>
        /// <param name="entity">题目</param>
        /// <returns></returns>
        public int AddTopic(TopicEntity entity)
        {
            return MyDbContext.SqlServerDb.Insertable(entity).ExecuteCommand();
        }
        /// <summary>
        /// 随机抽取数据
        /// </summary>
        /// <param name="Fraction">总分</param>
        /// <returns></returns>
        public List<TopicEntity> RandomTopic(int Fraction)
        {
            Random random = new Random();
            List<TopicEntity> list = new List<TopicEntity>();
            double frac = 0;
            ISugarQueryable<TopicEntity>? te = MyDbContext.SqlServerDb.Queryable<TopicEntity>();
            while (frac <= Fraction)
            {
                // 随机选择一个题目
                TopicEntity topic = te.OrderBy(t => random.Next()).First();
                list.Add(topic);
                frac = topic.TopicFraction;
            }
            return list;
        }
    }
}

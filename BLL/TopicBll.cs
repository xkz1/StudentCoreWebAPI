using DAL;

using Model.Entity;

namespace BLL
{
    public class TopicBll
    {
        TopicDal dal = new TopicDal();
        public int AddTopic(TopicEntity entity)
        {
            return dal.AddTopic(entity);
        }
        /// <summary>
        /// 随机抽取数据
        /// </summary>
        /// <param name="Fraction">总分</param>
        /// <returns></returns>
        public List<TopicEntity> RandomTopic(int Fraction) => dal.RandomTopic(Fraction);

    }
}

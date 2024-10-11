using DAL;

using Model.Entity;

namespace BLL
{
    public class ExaminBll
    {
        ExaminDal dal = new ExaminDal();
        public List<ExamEntity> QueayExam(string? sub = null, double scores = 0, DateTime? begin = null, DateTime? end = null, string topic = null)
        {
            return dal.QueayExam(sub, scores, begin, end, topic);
        }
        public List<TopicEntity> QueayTopic(string examId)
        {
            return dal.QueayTopic(examId);
        }
    }
}

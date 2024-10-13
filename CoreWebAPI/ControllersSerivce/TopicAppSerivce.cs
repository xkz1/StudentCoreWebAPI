using BLL;

using Mapster;

using Model.Dto;
using Model.Entity;

namespace CoreWebAPI.ControllersSerivce
{
    /// <summary>
    /// 题目
    /// </summary>
    public class TopicAppService : IAppService
    {
        TopicBll bll = new TopicBll();
        UserBll ub = new UserBll();
        Res dto = new Res();
        #region 获取数据
        /// <summary>
        /// 随机抽取数据
        /// </summary>
        /// <param name="Fraction">总分</param>
        /// <returns></returns>
        public Res RandomTopic(int Fraction)
        {
            try
            {
                List<InOToutTopicDto>? d = bll.RandomTopic(Fraction).Adapt<List<TopicEntity>, List<InOToutTopicDto>>();
                return dto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        #endregion
        #region 提交数据
        /// <summary>
        /// 添加题目
        /// </summary>
        /// <param name="topicDto"></param>
        /// <returns></returns>
        public Res AddInsertTopic(OutToInTopicDto topicDto)
        {
            try
            {
                TopicEntity entity = topicDto.Adapt<OutToInTopicDto, TopicEntity>();
                List<UserEntity> ue = ub.GetUserList(topicDto.CreateUserId);
                if (ue.Count > 0)
                    topicDto.CreateUserId = ue.FirstOrDefault().Id;
                dto.Data = bll.AddTopic(entity);
                return dto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        #endregion
        #region 私有方法

        #endregion
    }
}

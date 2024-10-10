using CoreWebAPI.ControllersSerivce;

using DAL;

using Microsoft.AspNetCore.Mvc;

using Model.Entity;

namespace CoreWebAPI.Controllers
{
    //[ApiController]
    //[Route("[controller]/[action]")]
    public class WeatherForecastController : IAppService
    {
        MyDbContext _db = new MyDbContext();
        [HttpGet]
        public void GetinitDb()
        {
            _db.CreateTable(false, 50, typeof(UserEntity));//�û�
            _db.CreateTable(false, 50, typeof(SgradeEntity));//�༶
            _db.CreateTable(false, 50, typeof(ExaminEntity));//����
            _db.CreateTable(false, 50, typeof(CriticismEntity));//��ʦ����
            _db.CreateTable(false, 50, typeof(TopicEntity));//��Ŀ

        }
    }
}

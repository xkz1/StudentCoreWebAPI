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
            _db.CreateTable(false, 50, typeof(UserEntity));//用户
            _db.CreateTable(false, 50, typeof(SgradeEntity));//班级
            _db.CreateTable(false, 50, typeof(ExaminEntity));//考试
            _db.CreateTable(false, 50, typeof(CriticismEntity));//老师判题
            _db.CreateTable(false, 50, typeof(TopicEntity));//题目

        }
    }
}

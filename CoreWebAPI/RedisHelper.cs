using StackExchange.Redis;

namespace CoreWebAPI
{
    public class RedisHelper
    {
        private static readonly ConfigurationOptions ConfigurationOptions = ConfigurationOptions.Parse("127.0.0.1:6379,password=123");
        private static readonly object Locker = new object();
        private static ConnectionMultiplexer _redisConn;

        /// <summary>
        /// 单例获取
        /// </summary>
        public static ConnectionMultiplexer RedisConn
        {
            get
            {
                if (_redisConn == null)
                {
                    // 锁定让同一时间只有一个线程访问该代码块
                    lock (Locker)
                    {
                        if (_redisConn == null || !_redisConn.IsConnected)
                        {
                            _redisConn = ConnectionMultiplexer.Connect(ConfigurationOptions);
                        }
                    }
                }
                return _redisConn;
            }
        }
    }
}

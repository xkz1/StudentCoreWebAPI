using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using Model.Entity;

using SqlSugar;

namespace DAL
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
        private readonly SqlSugarClient _db;

        public MyDbContext()
        {
            string connectionString = "Data Source=.;Initial Catalog=cs1;Integrated Security=True;Encrypt=False";
            // ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString;

            _db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    EntityService = (property, column) =>
                    {
                        var attributes = property.GetCustomAttributes(true);

                        if (attributes.Any(it => it is KeyAttribute))
                        {
                            column.IsPrimarykey = true;
                        }

                        // other attribute configurations
                    },
                    EntityNameService = (type, entity) =>
                    {
                        var attributes = type.GetCustomAttributes(true);
                        if (attributes.Any(it => it is TableAttribute))
                        {
                            var attr = attributes.First(it => it is TableAttribute) as TableAttribute;
                            entity.DbTableName = attr.Name;
                        }
                    }
                }
            });

            _db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine($"Executing SQL: {sql}");
                //Console.WriteLine($"Parameters: {pars.ToJson()}");
            };



        }

        public static SqlSugarScope SqlServerDb = new SqlSugarScope(new ConnectionConfig()
        {
            ConnectionString = "Data Source=.;Initial Catalog=cs1;Integrated Security=True;Encrypt=False",
            DbType = DbType.SqlServer,
            IsAutoCloseConnection = true
        });

        public void CreateTable(bool Backup = false, int StringDefaultLength = 50, params Type[] types)
        {
            _db.CodeFirst.SetStringDefaultLength(StringDefaultLength);
            _db.DbMaintenance.CreateDatabase();
            if (Backup)
            {
                _db.CodeFirst.BackupTable().InitTables(types);
            }
            else
            {
                _db.CodeFirst.InitTables(types);
            }
        }

        public SimpleClient<UserEntity> studentDb { get { return new SimpleClient<UserEntity>(_db); } }
    }
}

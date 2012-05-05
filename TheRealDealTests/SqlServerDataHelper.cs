using System.Data;
using RecreateMe.Locales;
using RecreateMe.Login;
using RecreateMe.Profiles;
using RecreateMe.Scheduling.Games;
using RecreateMe.Sports;
using RecreateMeSql;
using ServiceStack.OrmLite;

namespace TheRealDealTests
{
    public static class SqlServerDataHelper
    {
        private static readonly OrmLiteConnectionFactory ConnectionFactory;

        static SqlServerDataHelper()
        {
            ConnectionFactory = RecreateMeSql.ConnectionFactory.Create();

        }

         public static void DeleteAllData()
         {
                using (var db = ConnectionFactory.OpenDbConnection())   
                using (var dbCmd = db.CreateCommand())
                {
                    dbCmd.DeleteAll<PickUpGame>();
                    dbCmd.DeleteAll<Location>();
                    dbCmd.DeleteAll<Sport>();
                    dbCmd.DeleteAll<Profile>();
                    dbCmd.DeleteAll<Account>();
                }
         }
    }
}
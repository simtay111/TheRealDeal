

using RecreateMe.Locales;
using RecreateMe.Login;
using RecreateMe.Profiles;
using RecreateMe.Scheduling.Games;
using RecreateMe.Sports;
using RecreateMeSql.LinkingClasses;
using ServiceStack.OrmLite;

namespace RecreateMeSql
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
                    dbCmd.DeleteAll<PlayerLocationLink>();
                    dbCmd.DeleteAll<PlayerSportLink>();
                    dbCmd.DeleteAll<PlayerInGameLink>();
                    dbCmd.DeleteAll<PickUpGame>();
                    dbCmd.DeleteAll<Location>();
                    dbCmd.DeleteAll<Sport>();
                    dbCmd.DeleteAll<Profile>();
                    dbCmd.DeleteAll<Account>();
                }
         }

         public static void RebuildSchema()
         {
             using (var db = ConnectionFactory.OpenDbConnection())
             using (var dbCmd = db.CreateCommand())
             {
                 dbCmd.DropTable<PlayerLocationLink>();
                 dbCmd.DropTable<PlayerSportLink>();
                 dbCmd.DropTable<PlayerInGameLink>();
                 dbCmd.DropTable<PickUpGame>();
                 dbCmd.DropTable<Location>();
                 dbCmd.DropTable<Sport>();
                 dbCmd.DropTable<Profile>();
                 dbCmd.DropTable<Account>();

                 dbCmd.CreateTable<Account>(true);
                 dbCmd.CreateTable<Profile>(true);
                 dbCmd.CreateTable<Sport>(true);
                 dbCmd.CreateTable<Location>(true);
                 dbCmd.CreateTable<PickUpGame>(true);
                 dbCmd.CreateTable<PlayerInGameLink>(true);
                 dbCmd.CreateTable<PlayerSportLink>(true);
                 dbCmd.CreateTable<PlayerLocationLink>(true);
             }
         }

         public static void BuildSchema()
         {
             using (var db = ConnectionFactory.OpenDbConnection())
             using (var dbCmd = db.CreateCommand())
             {
                 dbCmd.CreateTable<Account>();
                 dbCmd.CreateTable<Profile>();
                 dbCmd.CreateTable<Sport>();
                 dbCmd.CreateTable<Location>();
                 dbCmd.CreateTable<PickUpGame>();
                 dbCmd.CreateTable<PlayerInGameLink>();
                 dbCmd.CreateTable<PlayerSportLink>();
                 dbCmd.CreateTable<PlayerLocationLink>();
             }
         }
    }
}
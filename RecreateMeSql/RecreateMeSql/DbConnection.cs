namespace RecreateMeSql
{
    public class DbConnection
    {
        private static DbConnection _connection;

        private DbConnection()
        {
        }

        public static DbConnection Instance()
        {
            if (_connection == null)
            {
                _connection = new DbConnection();
            }

            return _connection;
        }
}
}
namespace Rex.Common.Data
{
    public class TablePrimaryKeys
    {
        public string TableName { get; private set; }

        public PrimaryKeySet PrimaryKeys { get; private set; }

		public TablePrimaryKeys(string tableName, PrimaryKeySet primaryKeys)
        {
            this.TableName = tableName;
            this.PrimaryKeys = primaryKeys;
        }
    }
}

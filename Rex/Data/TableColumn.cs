namespace Rex.Common.Data
{
    public class TableColumn
    {
        public TableColumn(string table, string column, string schema = "dbo")
        {
            this.Table = table;
            this.Column = column;
            this.Schema = schema;
        }

        public string Schema { private set; get; }

        public string Table { get; private set; }

        public string Column { get; private set; }
    }
}

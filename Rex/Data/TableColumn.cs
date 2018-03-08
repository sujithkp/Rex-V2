namespace Rex.Common.Data
{
    public class TableColumn
    {
        public TableColumn(string table, string column)
        {
            this.Table = table;
            this.Column = column;
        }

        public string Table { get; private set; }

        public string Column { get; private set; }
    }
}

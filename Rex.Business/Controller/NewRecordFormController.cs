using Rex.Business.Store;
using Rex.Business.UI;
using Rex.Common.Data;
using System.Collections.Generic;

namespace Rex.Business.Controller
{
    public class NewRecordFormController
    {
        private AddRecordWindow View;

        private InformationSchema _schema;

        public NewRecordFormController(InformationSchema schema)
        {
            this.View = new AddRecordWindow(this);
            _schema = schema;
        }

        public TablePrimaryKeys GetRecordPrimaryKeySet()
        {
            this.View.GetPrimaryKeys(_schema.GetAllTables());
            return this.View.Result;
        }

        public IEnumerable<string> GetPrimaryKeys(string table)
        {
            return _schema.GetPrimaryColumns(table);
        }

    }
}

using Rex.Business.UI;
using System.Linq;

namespace Rex.Business.Controller
{
    public class TableSelectorController
    {
        public ITableSelector View = new TableSelectorWindow();

        private DataStore _store;

        public TableSelectorController(DataStore store)
        {
            _store = store;
        }

        public string GetUserSelection()
        {
            return this.View.GetSelectedTable(_store.GetAllTables().ToList());
        }
    }
}

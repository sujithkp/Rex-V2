using Rex.Common.Data;
using System;

namespace Rex.UI.Lib
{
    public class LinkEventArgs : EventArgs
    {
        public LinkEventArgs(string selectedTable, PrimaryKeySet primaryKeys)
        {
            this.SelectedTable = selectedTable;

            this.PrimaryKeys = primaryKeys;
        }

        public string SelectedTable { get; private set; }

        public PrimaryKeySet PrimaryKeys { get; private set; }
    }
}

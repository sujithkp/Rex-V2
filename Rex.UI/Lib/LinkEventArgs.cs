using System;

namespace Rex.UI.Lib
{
    public class LinkEventArgs : EventArgs
    {
        public LinkEventArgs(string selectedTable)
        {
            this.SelectedTable = selectedTable;
        }

        public string SelectedTable { get; private set; }
    }
}

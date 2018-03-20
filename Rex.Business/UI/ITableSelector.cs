using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rex.Business.UI
{
    public interface ITableSelector
    {
        string GetSelectedTable(IList<string> tables);
    }
}

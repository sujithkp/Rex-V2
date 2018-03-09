using Rex.Business.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rex.Business.Controller
{
    public class NewRecordFormController
    {
        private AddRecordForm View;

        public NewRecordFormController()
        {
            this.View = new AddRecordForm(this);
        }

        public void GetRecordPrimaryKeySet()
        {
            this.View.ShowDialog();

        }


    }
}

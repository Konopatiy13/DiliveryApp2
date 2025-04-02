using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiliveryApp2._0.ViewModel
{
    public class TableDataViewModel
    {
        public string TableName { get; set; }
        public IEnumerable<object> Data { get; set; }
    }

}

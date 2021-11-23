using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseControl.DBClasses;

namespace RestApiServer.Models
{
    public class RowModel : RestModelBase
    {
        public List<string> Row { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace CSharpFeatures.MEF
{
    public interface ISalesOrderView { }

    [Export(typeof(ISalesOrderView))]
    public partial class SalesOrderView ﻿﻿: ISalesOrderView
    {
        //...

    }

    [Export]
    public class ViewFactory
    {
        [Import]
        ﻿ISalesOrderView ﻿OrderView { get; set; }
    }
}

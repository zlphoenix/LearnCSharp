using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proxy
{
    public static class MyTransparentProxy
    { 
        public static Ttargat Create<Ttargat>(Ttargat instance)
        { 
            MyRealProxy<Ttargat> realProxy = new MyRealProxy<Ttargat>(instance);
            Ttargat transparentProxy = (Ttargat)realProxy.GetTransparentProxy();
            return transparentProxy;
        }
    }
}

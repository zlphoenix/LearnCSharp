using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SolutionMaker.Core;

namespace SolutionMaker.UI
{
    public class StatusLogger : ILogger
    {
        private Control _control;

        public StatusLogger(Control control)
        {
            this._control = control;
        }

        #region ILogger Members

        public void Write(string text)
        {
            this._control.Text = text;
            this._control.Update();
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redirect_projet_libre
{
    enum messageType_
    {
        ERROR,
        WARNING,
        NORMAL
    };

    class TraceCompil
    {
        #region Variables

        public messageType_ type_ { get; set; }

        public string lineNumber_ { get; set; }
        public string message_ { get; set; }
        public string fileName_ { get; set; }

        #endregion

        public TraceCompil(messageType_ type, string lineNumber, string message, string fileName)
        {
            type_ = type;
            lineNumber_ = lineNumber;
            message_ = message;
            fileName_ = fileName;
        }
    }
}

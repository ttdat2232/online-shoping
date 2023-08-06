using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ApplicationException : Exception 
    {
        public ApplicationException() : base() { }
        public ApplicationException(string message) : base(message) { }
        public ApplicationException(string message, params object[] args) : base (string.Format(CultureInfo.CurrentCulture ,message, args)) { }
    }
}

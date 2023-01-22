using System;

namespace Harpoon.Application
{
    public class ActionParameterNullException : Exception
    {
        public ActionParameterNullException(string paramName)
            : base(string.Format("Action parameter named '{0}' can't be null or empty", paramName))
        {
        }
        
    }
}
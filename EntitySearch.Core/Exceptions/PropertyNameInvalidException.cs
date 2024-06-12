using System;

namespace EntitySearch.Core.Exceptions {
    public class PropertyNameInvalidException: Exception
    { 
        public PropertyNameInvalidException(string msg)
            : base(msg)
        { }
    }
}

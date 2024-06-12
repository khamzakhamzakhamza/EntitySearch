using System;

namespace EntitySearch.Core.Exceptions {
    public class PropertyNameNotProvidedException: Exception
    { 
        public PropertyNameNotProvidedException(string msg)
            : base(msg)
        { }
    }
}

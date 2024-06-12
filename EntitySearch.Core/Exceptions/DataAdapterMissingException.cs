using System;

namespace EntitySearch.Core.Exceptions {
    public class DataAdapterMissingException: Exception
    { 
        public DataAdapterMissingException(string msg)
            : base(msg)
        { }
    }
}

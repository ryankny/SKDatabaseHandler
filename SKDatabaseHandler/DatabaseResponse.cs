using System;
using System.Collections.Generic;
using System.Text;

namespace SKDatabaseHandler
{
    public class DatabaseResponse
    {
        public dynamic ResponseObject { get; set; }
        public Exception ErrorException { get; set; }

        public DatabaseResponse(dynamic responseObject, Exception errorException = null)
        {
            ResponseObject = responseObject;
            ErrorException = errorException;
        }

        public bool HasErrorOccured()
        {
            return (ErrorException != null);
        }
    }
}

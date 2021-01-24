using System;
using System.Collections.Generic;
using System.Text;

namespace SKDatabaseHandler
{
    public class DatabaseConnectionTest
    {
        public bool Successful { get; set; }
        public Exception ErrorException { get; set; }

        public DatabaseConnectionTest(bool successful, Exception errorException = null)
        {
            Successful = successful;
            ErrorException = errorException;
        }

        public bool HasErrorOccured()
        {
            return (ErrorException != null);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.GCommon.Exceptions
{
    public class DatabaseEntityCreatePersistFailureException :Exception
    {
        public DatabaseEntityCreatePersistFailureException()
        {
            
        }

        public DatabaseEntityCreatePersistFailureException(string message)
            : base(message) 
        {
            
        }

    }
}

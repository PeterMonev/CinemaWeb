using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.GCommon.Exceptions
{
    public class EntityPersistFailureException :Exception
    {
        public EntityPersistFailureException()
        {
            
        }

        public EntityPersistFailureException(string message)
            : base(message) 
        {
            
        }

    }
}

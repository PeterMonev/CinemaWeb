namespace CinemaApp.GCommon.Exceptions
{
    public class EntityKeyNullOrEmptyException : Exception
    {
        public EntityKeyNullOrEmptyException()
        {

        }

        public EntityKeyNullOrEmptyException(string message)
            : base(message)
        {

        }
    }
}
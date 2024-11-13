namespace Note_App_API.Exceptions
{
    public class ForbidAccessException : Exception
    {
        public ForbidAccessException(string message) : base(message) { }
    }
}

namespace Test.Exceptions
{
    public class InvalidWordException : Exception
    {
        public InvalidWordException(string? message) : base($"Invalid word '{message}'.")
        {
        }
    }
}
